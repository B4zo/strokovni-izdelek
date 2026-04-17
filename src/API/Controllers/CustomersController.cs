using API.Data;
using API.DTOs.Customers;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/customers")]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _db;
    public CustomersController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> Get(
        [FromQuery] string? q = null,
        [FromQuery] string? type = null,
        [FromQuery] string? address = null,
        [FromQuery] string? phone = null,
        [FromQuery] string? email = null,
        [FromQuery] string? name = null,
        [FromQuery] string? taxNumber = null,
        [FromQuery] string? registrationNo = null)
    {
        var query = _db.Customers.AsNoTracking().Include(x => x.Person).Include(x => x.Company).AsQueryable();

        if (!string.IsNullOrWhiteSpace(type))
        {
            type = type.Trim().ToLowerInvariant();
            if (type is "person" or "company")
                query = query.Where(x => x.Type == type);
        }

        if (!string.IsNullOrWhiteSpace(address))
        {
            address = address.Trim();
            query = query.Where(x => x.Address != null && EF.Functions.ILike(x.Address, $"%{address}%"));
        }

        if (!string.IsNullOrWhiteSpace(phone))
        {
            phone = phone.Trim();
            query = query.Where(x => x.Phone != null && EF.Functions.ILike(x.Phone, $"%{phone}%"));
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            email = email.Trim();
            query = query.Where(x => x.Email != null && EF.Functions.ILike(x.Email, $"%{email}%"));
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            name = name.Trim();
            query = query.Where(x =>
                (x.Person != null && EF.Functions.ILike(x.Person.FullName, $"%{name}%")) ||
                (x.Company != null && EF.Functions.ILike(x.Company.CompanyName, $"%{name}%")));
        }

        if (!string.IsNullOrWhiteSpace(taxNumber))
        {
            taxNumber = taxNumber.Trim();
            query = query.Where(x =>
                (x.Person != null && x.Person.TaxNumber != null && EF.Functions.ILike(x.Person.TaxNumber, $"%{taxNumber}%")) ||
                (x.Company != null && x.Company.TaxNumber != null && EF.Functions.ILike(x.Company.TaxNumber, $"%{taxNumber}%")));
        }

        if (!string.IsNullOrWhiteSpace(registrationNo))
        {
            registrationNo = registrationNo.Trim();
            query = query.Where(x => x.Company != null && x.Company.RegistrationNo != null && EF.Functions.ILike(x.Company.RegistrationNo, $"%{registrationNo}%"));
        }

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            query = query.Where(x =>
                (x.Address != null && EF.Functions.ILike(x.Address, $"%{q}%")) ||
                (x.Phone != null && EF.Functions.ILike(x.Phone, $"%{q}%")) ||
                (x.Email != null && EF.Functions.ILike(x.Email, $"%{q}%")) ||
                (x.Person != null && EF.Functions.ILike(x.Person.FullName, $"%{q}%")) ||
                (x.Company != null && EF.Functions.ILike(x.Company.CompanyName, $"%{q}%")) ||
                (x.Person != null && x.Person.TaxNumber != null && EF.Functions.ILike(x.Person.TaxNumber, $"%{q}%")) ||
                (x.Company != null && x.Company.TaxNumber != null && EF.Functions.ILike(x.Company.TaxNumber, $"%{q}%")) ||
                (x.Company != null && x.Company.RegistrationNo != null && EF.Functions.ILike(x.Company.RegistrationNo, $"%{q}%")));
        }

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => ToDto(x))
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerDto>> GetById(Guid id)
    {
        var item = await _db.Customers.AsNoTracking().Include(x => x.Person).Include(x => x.Company).SingleOrDefaultAsync(x => x.Id == id);
        return item is null ? NotFound() : Ok(ToDto(item));
    }

    [HttpGet("{id:guid}/details")]
    public async Task<ActionResult<CustomerDetailDto>> GetDetails(Guid id)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var customer = await _db.Customers
            .AsNoTracking()
            .Include(x => x.Person)
            .Include(x => x.Company)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (customer is null)
            return NotFound();

        var ownedVehicles = await _db.VehicleOwnerships
            .AsNoTracking()
            .Where(x => x.CustomerId == id)
            .Include(x => x.Vehicle)
            .OrderByDescending(x => x.ValidFrom)
            .Select(x => new CustomerOwnedVehicleDto(
                x.VehicleId,
                x.Vehicle.Vin,
                ((x.Vehicle.Make ?? "") + " " + (x.Vehicle.Model ?? "")).Trim(),
                x.ValidFrom,
                x.ValidTo,
                x.ValidFrom <= today && (x.ValidTo == null || x.ValidTo >= today)))
            .ToListAsync();

        var registeredVehicles = await _db.VehicleRegistrations
            .AsNoTracking()
            .Where(x => x.CustomerId == id)
            .Include(x => x.Vehicle)
            .Include(x => x.PlateAssignments).ThenInclude(x => x.Plate)
            .OrderByDescending(x => x.ValidFrom)
            .Select(x => new CustomerRegisteredVehicleDto(
                x.Id,
                x.VehicleId,
                x.Vehicle.Vin,
                ((x.Vehicle.Make ?? "") + " " + (x.Vehicle.Model ?? "")).Trim(),
                x.RegistrationNo,
                x.PlateAssignments.OrderByDescending(pa => pa.ValidFrom).Select(pa => pa.Plate.PlateNumber).FirstOrDefault(),
                x.ValidFrom,
                x.ValidTo,
                x.ValidFrom <= today && (x.ValidTo == null || x.ValidTo >= today)))
            .ToListAsync();

        return Ok(new CustomerDetailDto(ToDto(customer), ownedVehicles, registeredVehicles));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> Create(CustomerUpsertRequest req)
    {
        if (!IsValidType(req.Type))
            return BadRequest("Type must be 'person' or 'company'.");

        var customer = new Customers
        {
            Type = req.Type,
            Address = req.Address,
            Phone = req.Phone,
            Email = req.Email
        };

        if (req.Type == "person")
        {
            customer.Person = new People
            {
                FullName = req.FullName ?? "",
                DateOfBirth = req.DateOfBirth,
                TaxNumber = req.TaxNumber,
                NationalNo = req.NationalNo
            };
        }
        else
        {
            customer.Company = new Companies
            {
                CompanyName = req.CompanyName ?? "",
                TaxNumber = req.TaxNumber,
                RegistrationNo = req.RegistrationNo
            };
        }

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();

        var created = await _db.Customers.AsNoTracking().Include(x => x.Person).Include(x => x.Company).SingleAsync(x => x.Id == customer.Id);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, ToDto(created));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, CustomerUpsertRequest req)
    {
        if (!IsValidType(req.Type))
            return BadRequest("Type must be 'person' or 'company'.");

        var customer = await _db.Customers.Include(x => x.Person).Include(x => x.Company).SingleOrDefaultAsync(x => x.Id == id);
        if (customer is null) return NotFound();

        customer.Type = req.Type;
        customer.Address = req.Address;
        customer.Phone = req.Phone;
        customer.Email = req.Email;

        customer.Person = req.Type == "person"
            ? new People
            {
                CustomerId = customer.Id,
                FullName = req.FullName ?? "",
                DateOfBirth = req.DateOfBirth,
                TaxNumber = req.TaxNumber,
                NationalNo = req.NationalNo
            }
            : null;

        customer.Company = req.Type == "company"
            ? new Companies
            {
                CustomerId = customer.Id,
                CompanyName = req.CompanyName ?? "",
                TaxNumber = req.TaxNumber,
                RegistrationNo = req.RegistrationNo
            }
            : null;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var customer = await _db.Customers.SingleOrDefaultAsync(x => x.Id == id);
        if (customer is null) return NotFound();

        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private static CustomerDto ToDto(Customers x) =>
        new(
            x.Id,
            x.Type,
            x.Address,
            x.Phone,
            x.Email,
            x.CreatedAt,
            x.Person is null ? null : new PersonDto(x.Person.CustomerId, x.Person.FullName, x.Person.DateOfBirth, x.Person.TaxNumber, x.Person.NationalNo),
            x.Company is null ? null : new CompanyDto(x.Company.CustomerId, x.Company.CompanyName, x.Company.TaxNumber, x.Company.RegistrationNo)
        );

    private static bool IsValidType(string type)
        => type is "person" or "company";
}
