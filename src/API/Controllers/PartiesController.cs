using API.Data;
using API.DTOs.Parties;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/parties")]
[Authorize]
public class PartiesController : ControllerBase
{
    private readonly AppDbContext _db;

    public PartiesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PartyDto>>> Get(
        [FromQuery] string? q = null,
        [FromQuery] string? type = null,
        [FromQuery] string? address = null,
        [FromQuery] string? phone = null,
        [FromQuery] string? email = null,
        [FromQuery] string? name = null,
        [FromQuery] string? taxNo = null,
        [FromQuery] string? companyRegNo = null,
        [FromQuery] string? emso = null)
    {
        var query = _db.Parties
            .AsNoTracking()
            .Include(x => x.Person)
            .Include(x => x.Company)
            .AsQueryable();

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

        if (!string.IsNullOrWhiteSpace(taxNo))
        {
            taxNo = taxNo.Trim();
            query = query.Where(x =>
                (x.Person != null && x.Person.TaxNo != null && EF.Functions.ILike(x.Person.TaxNo, $"%{taxNo}%")) ||
                (x.Company != null && x.Company.TaxNo != null && EF.Functions.ILike(x.Company.TaxNo, $"%{taxNo}%")));
        }

        if (!string.IsNullOrWhiteSpace(companyRegNo))
        {
            companyRegNo = companyRegNo.Trim();
            query = query.Where(x =>
                x.Company != null &&
                x.Company.CompanyRegNo != null &&
                EF.Functions.ILike(x.Company.CompanyRegNo, $"%{companyRegNo}%"));
        }

        if (!string.IsNullOrWhiteSpace(emso))
        {
            emso = emso.Trim();
            query = query.Where(x =>
                x.Person != null &&
                x.Person.Emso != null &&
                EF.Functions.ILike(x.Person.Emso, $"%{emso}%"));
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
                (x.Person != null && x.Person.TaxNo != null && EF.Functions.ILike(x.Person.TaxNo, $"%{q}%")) ||
                (x.Person != null && x.Person.Emso != null && EF.Functions.ILike(x.Person.Emso, $"%{q}%")) ||
                (x.Company != null && x.Company.TaxNo != null && EF.Functions.ILike(x.Company.TaxNo, $"%{q}%")) ||
                (x.Company != null && x.Company.CompanyRegNo != null && EF.Functions.ILike(x.Company.CompanyRegNo, $"%{q}%")));
        }

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => ToDto(x))
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PartyDto>> GetById(Guid id)
    {
        var item = await _db.Parties
            .AsNoTracking()
            .Include(x => x.Person)
            .Include(x => x.Company)
            .SingleOrDefaultAsync(x => x.Id == id);

        return item is null ? NotFound() : Ok(ToDto(item));
    }

    [HttpGet("{id:guid}/details")]
    public async Task<ActionResult<PartyDetailDto>> GetDetails(Guid id)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var party = await _db.Parties
            .AsNoTracking()
            .Include(x => x.Person)
            .Include(x => x.Company)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (party is null)
            return NotFound();

        var ownedVehicles = await _db.VehicleOwnerships
            .AsNoTracking()
            .Where(x => x.PartyId == id)
            .Include(x => x.Vehicle)
            .OrderByDescending(x => x.ValidFrom)
            .Select(x => new PartyOwnedVehicleDto(
                x.VehicleId,
                x.Vehicle.Vin,
                ((x.Vehicle.Make ?? "") + " " + (x.Vehicle.Model ?? "")).Trim(),
                x.ValidFrom,
                x.ValidTo,
                x.ValidFrom <= today && (x.ValidTo == null || x.ValidTo >= today)))
            .ToListAsync();

        var registeredVehicles = await _db.VehicleRegistrations
            .AsNoTracking()
            .Where(x => x.PartyId == id)
            .Include(x => x.Vehicle)
            .Include(x => x.PlateAssignments)
            .ThenInclude(x => x.Plate)
            .OrderByDescending(x => x.ValidFrom)
            .Select(x => new PartyRegisteredVehicleDto(
                x.Id,
                x.VehicleId,
                x.Vehicle.Vin,
                ((x.Vehicle.Make ?? "") + " " + (x.Vehicle.Model ?? "")).Trim(),
                x.RegistrationNo,
                x.PlateAssignments
                    .OrderByDescending(pa => pa.ValidFrom)
                    .Select(pa => pa.Plate.PlateNo)
                    .FirstOrDefault(),
                x.ValidFrom,
                x.ValidTo,
                x.ValidFrom <= today && (x.ValidTo == null || x.ValidTo >= today)))
            .ToListAsync();

        return Ok(new PartyDetailDto(ToDto(party), ownedVehicles, registeredVehicles));
    }

    [HttpPost]
    public async Task<ActionResult<PartyDto>> Create(PartyUpsertRequest req)
    {
        if (!IsValidType(req.Type))
            return BadRequest("Type must be 'person' or 'company'.");

        var party = new Party
        {
            Type = req.Type,
            Address = req.Address,
            Phone = req.Phone,
            Email = req.Email
        };

        if (req.Type == "person")
        {
            party.Person = new Person
            {
                FullName = req.FullName ?? "",
                DateOfBirth = req.DateOfBirth,
                TaxNo = req.TaxNo,
                Emso = req.Emso
            };
        }
        else
        {
            party.Company = new Company
            {
                CompanyName = req.CompanyName ?? "",
                TaxNo = req.TaxNo,
                CompanyRegNo = req.CompanyRegNo
            };
        }

        _db.Parties.Add(party);
        await _db.SaveChangesAsync();

        var created = await _db.Parties
            .AsNoTracking()
            .Include(x => x.Person)
            .Include(x => x.Company)
            .SingleAsync(x => x.Id == party.Id);

        return CreatedAtAction(nameof(GetById), new { id = party.Id }, ToDto(created));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, PartyUpsertRequest req)
    {
        if (!IsValidType(req.Type))
            return BadRequest("Type must be 'person' or 'company'.");

        var party = await _db.Parties
            .Include(x => x.Person)
            .Include(x => x.Company)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (party is null)
            return NotFound();

        party.Type = req.Type;
        party.Address = req.Address;
        party.Phone = req.Phone;
        party.Email = req.Email;

        party.Person = req.Type == "person"
            ? new Person
            {
                PartyId = party.Id,
                FullName = req.FullName ?? "",
                DateOfBirth = req.DateOfBirth,
                TaxNo = req.TaxNo,
                Emso = req.Emso
            }
            : null;

        party.Company = req.Type == "company"
            ? new Company
            {
                PartyId = party.Id,
                CompanyName = req.CompanyName ?? "",
                TaxNo = req.TaxNo,
                CompanyRegNo = req.CompanyRegNo
            }
            : null;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var party = await _db.Parties.SingleOrDefaultAsync(x => x.Id == id);
        if (party is null)
            return NotFound();

        _db.Parties.Remove(party);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private static PartyDto ToDto(Party party) =>
        new(
            party.Id,
            party.Type,
            party.Address,
            party.Phone,
            party.Email,
            party.CreatedAt,
            party.Person is null
                ? null
                : new PersonDto(
                    party.Person.PartyId,
                    party.Person.FullName,
                    party.Person.DateOfBirth,
                    party.Person.TaxNo,
                    party.Person.Emso),
            party.Company is null
                ? null
                : new CompanyDto(
                    party.Company.PartyId,
                    party.Company.CompanyName,
                    party.Company.TaxNo,
                    party.Company.CompanyRegNo)
        );

    private static bool IsValidType(string type) => type is "person" or "company";
}

