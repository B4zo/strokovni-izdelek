using API.Data;
using API.DTOs.Vehicles;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/vehicles")]
[Authorize]
public class VehiclesController : ControllerBase
{
    private readonly AppDbContext _db;
    public VehiclesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleDto>>> Get(
        [FromQuery] string? q = null,
        [FromQuery] string? vin = null,
        [FromQuery] string? make = null,
        [FromQuery] string? model = null,
        [FromQuery] string? category = null,
        [FromQuery] int? year = null)
    {
        var query = _db.Vehicles.AsNoTracking().Include(x => x.Category).AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            query = query.Where(x =>
                EF.Functions.ILike(x.Vin, $"%{q}%") ||
                (x.Make != null && EF.Functions.ILike(x.Make, $"%{q}%")) ||
                (x.Model != null && EF.Functions.ILike(x.Model, $"%{q}%")) ||
                EF.Functions.ILike(x.Category.Code, $"%{q}%") ||
                EF.Functions.ILike(x.Category.Label, $"%{q}%"));
        }

        if (!string.IsNullOrWhiteSpace(vin))
        {
            vin = vin.Trim();
            query = query.Where(x => EF.Functions.ILike(x.Vin, $"%{vin}%"));
        }

        if (!string.IsNullOrWhiteSpace(make))
        {
            make = make.Trim();
            query = query.Where(x => x.Make != null && EF.Functions.ILike(x.Make, $"%{make}%"));
        }

        if (!string.IsNullOrWhiteSpace(model))
        {
            model = model.Trim();
            query = query.Where(x => x.Model != null && EF.Functions.ILike(x.Model, $"%{model}%"));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            category = category.Trim();
            query = query.Where(x =>
                EF.Functions.ILike(x.Category.Code, $"%{category}%") ||
                EF.Functions.ILike(x.Category.Label, $"%{category}%"));
        }

        if (year is not null)
            query = query.Where(x => x.Year == year.Value);

        var today = DateOnly.FromDateTime(DateTime.Today);

        var items = await query
            .OrderBy(x => x.Vin)
            .Select(x => new VehicleDto(
                x.Id,
                x.Vin,
                x.CategoryId,
                x.Category.Code,
                x.Category.Label,
                x.Make,
                x.Model,
                x.Year,
                x.Notes,
                x.OwnershipHistory
                    .Where(h => h.ValidFrom <= today && (h.ValidTo == null || h.ValidTo >= today))
                    .OrderByDescending(h => h.ValidFrom)
                    .Select(h => h.Party.Person != null ? h.Party.Person.FullName : h.Party.Company != null ? h.Party.Company.CompanyName : "")
                    .FirstOrDefault(),
                x.Registrations
                    .Where(r => r.ValidFrom <= today && (r.ValidTo == null || r.ValidTo >= today))
                    .OrderByDescending(r => r.ValidFrom)
                    .Select(r => r.RegistrationNo)
                    .FirstOrDefault(),
                x.Registrations
                    .Where(r => r.ValidFrom <= today && (r.ValidTo == null || r.ValidTo >= today))
                    .SelectMany(r => r.PlateAssignments)
                    .OrderByDescending(pa => pa.ValidFrom)
                    .Select(pa => pa.Plate.PlateNo)
                    .FirstOrDefault()))
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:guid}/details")]
    public async Task<ActionResult<VehicleDetailDto>> GetDetails(Guid id)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var vehicle = await _db.Vehicles
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.OwnershipHistory).ThenInclude(x => x.Party).ThenInclude(x => x.Person)
            .Include(x => x.OwnershipHistory).ThenInclude(x => x.Party).ThenInclude(x => x.Company)
            .Include(x => x.Registrations).ThenInclude(x => x.Party).ThenInclude(x => x.Person)
            .Include(x => x.Registrations).ThenInclude(x => x.Party).ThenInclude(x => x.Company)
            .Include(x => x.Registrations).ThenInclude(x => x.PlateAssignments).ThenInclude(x => x.Plate)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (vehicle is null)
            return NotFound();

        var dto = new VehicleDetailDto(
            new VehicleDto(
                vehicle.Id,
                vehicle.Vin,
                vehicle.CategoryId,
                vehicle.Category.Code,
                vehicle.Category.Label,
                vehicle.Make,
                vehicle.Model,
                vehicle.Year,
                vehicle.Notes,
                vehicle.OwnershipHistory
                    .Where(h => h.ValidFrom <= today && (h.ValidTo == null || h.ValidTo >= today))
                    .OrderByDescending(h => h.ValidFrom)
                    .Select(h => h.Party.Person != null ? h.Party.Person.FullName : h.Party.Company != null ? h.Party.Company.CompanyName : "")
                    .FirstOrDefault(),
                vehicle.Registrations
                    .Where(r => r.ValidFrom <= today && (r.ValidTo == null || r.ValidTo >= today))
                    .OrderByDescending(r => r.ValidFrom)
                    .Select(r => r.RegistrationNo)
                    .FirstOrDefault(),
                vehicle.Registrations
                    .Where(r => r.ValidFrom <= today && (r.ValidTo == null || r.ValidTo >= today))
                    .SelectMany(r => r.PlateAssignments)
                    .OrderByDescending(pa => pa.ValidFrom)
                    .Select(pa => pa.Plate.PlateNo)
                    .FirstOrDefault()),
            vehicle.OwnershipHistory
                .OrderByDescending(x => x.ValidFrom)
                .Select(x => new VehicleOwnerHistoryDto(
                    x.Id,
                    x.PartyId,
                    x.Party.Person != null ? x.Party.Person.FullName : x.Party.Company != null ? x.Party.Company.CompanyName : "",
                    x.ValidFrom,
                    x.ValidTo,
                    x.ValidFrom <= today && (x.ValidTo == null || x.ValidTo >= today)))
                .ToList(),
            vehicle.Registrations
                .OrderByDescending(x => x.ValidFrom)
                .Select(x => new VehicleRegistrationHistoryDto(
                    x.Id,
                    x.PartyId,
                    x.Party.Person != null ? x.Party.Person.FullName : x.Party.Company != null ? x.Party.Company.CompanyName : "",
                    x.RegistrationNo,
                    x.PlateAssignments.OrderByDescending(pa => pa.ValidFrom).Select(pa => pa.Plate.PlateNo).FirstOrDefault(),
                    x.ValidFrom,
                    x.ValidTo,
                    x.ValidFrom <= today && (x.ValidTo == null || x.ValidTo >= today)))
                .ToList());

        return Ok(dto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Vehicle>> GetById(Guid id)
    {
        var item = await _db.Vehicles.AsNoTracking().Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Vehicle>> Create(VehicleUpsertRequest req)
    {
        var vehicle = new Vehicle
        {
            Vin = req.Vin.Trim().ToUpperInvariant(),
            CategoryId = req.CategoryId,
            Make = req.Make,
            Model = req.Model,
            Year = req.Year,
            Notes = req.Notes
        };

        _db.Vehicles.Add(vehicle);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, VehicleUpsertRequest req)
    {
        var vehicle = await _db.Vehicles.SingleOrDefaultAsync(x => x.Id == id);
        if (vehicle is null) return NotFound();

        vehicle.Vin = req.Vin.Trim().ToUpperInvariant();
        vehicle.CategoryId = req.CategoryId;
        vehicle.Make = req.Make;
        vehicle.Model = req.Model;
        vehicle.Year = req.Year;
        vehicle.Notes = req.Notes;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var vehicle = await _db.Vehicles.SingleOrDefaultAsync(x => x.Id == id);
        if (vehicle is null) return NotFound();

        _db.Vehicles.Remove(vehicle);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

