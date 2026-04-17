using API.Data;
using API.DTOs.Registrations;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/registrations")]
[Authorize]
public class RegistrationsController : ControllerBase
{
    private readonly AppDbContext _db;
    public RegistrationsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] string? plate = null,
        [FromQuery] string? registrationNo = null,
        [FromQuery] string? vin = null,
        [FromQuery] string? customer = null,
        [FromQuery] Guid? vehicleId = null,
        [FromQuery] Guid? customerId = null,
        [FromQuery] bool currentOnly = false)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var query = _db.VehicleRegistrations
            .AsNoTracking()
            .Include(x => x.Vehicle)
            .Include(x => x.Customer).ThenInclude(x => x.Person)
            .Include(x => x.Customer).ThenInclude(x => x.Company)
            .Include(x => x.PlateAssignments).ThenInclude(x => x.Plate)
            .AsQueryable();

        if (vehicleId is not null) query = query.Where(x => x.VehicleId == vehicleId);
        if (customerId is not null) query = query.Where(x => x.CustomerId == customerId);
        if (!string.IsNullOrWhiteSpace(plate))
        {
            plate = plate.Trim();
            query = query.Where(x => x.PlateAssignments.Any(pa => EF.Functions.ILike(pa.Plate.PlateNumber, $"%{plate}%")));
        }
        if (!string.IsNullOrWhiteSpace(registrationNo))
        {
            registrationNo = registrationNo.Trim();
            query = query.Where(x => x.RegistrationNo != null && EF.Functions.ILike(x.RegistrationNo, $"%{registrationNo}%"));
        }
        if (!string.IsNullOrWhiteSpace(vin))
        {
            vin = vin.Trim();
            query = query.Where(x => EF.Functions.ILike(x.Vehicle.Vin, $"%{vin}%"));
        }
        if (!string.IsNullOrWhiteSpace(customer))
        {
            customer = customer.Trim();
            query = query.Where(x =>
                (x.Customer.Person != null && EF.Functions.ILike(x.Customer.Person.FullName, $"%{customer}%")) ||
                (x.Customer.Company != null && EF.Functions.ILike(x.Customer.Company.CompanyName, $"%{customer}%")));
        }
        if (currentOnly)
            query = query.Where(x => x.ValidFrom <= today && (x.ValidTo == null || x.ValidTo >= today));

        var items = await query
            .OrderByDescending(x => x.ValidFrom)
            .Select(x => new RegistrationDto(
                x.Id,
                x.VehicleId,
                x.Vehicle.Vin,
                ((x.Vehicle.Make ?? "") + " " + (x.Vehicle.Model ?? "")).Trim(),
                x.CustomerId,
                x.Customer.Person != null ? x.Customer.Person.FullName : x.Customer.Company != null ? x.Customer.Company.CompanyName : "",
                x.RegistrationNo,
                x.PlateAssignments
                    .OrderByDescending(pa => pa.ValidFrom)
                    .Select(pa => pa.Plate.PlateNumber)
                    .FirstOrDefault(),
                x.ValidFrom,
                x.ValidTo,
                x.ValidFrom <= today && (x.ValidTo == null || x.ValidTo >= today),
                x.Notes))
            .ToListAsync();

        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> Create(VehicleRegistrationUpsertRequest req)
    {
        var entity = new VehicleRegistration
        {
            VisitOperationId = req.VisitOperationId,
            VehicleId = req.VehicleId,
            CustomerId = req.CustomerId,
            ValidFrom = req.ValidFrom,
            ValidTo = req.ValidTo,
            RegistrationNo = req.RegistrationNo,
            Notes = req.Notes
        };
        _db.VehicleRegistrations.Add(entity);
        await _db.SaveChangesAsync();
        return Ok(entity);
    }
}
