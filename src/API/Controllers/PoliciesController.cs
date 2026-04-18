using API.Data;
using API.DTOs.Policies;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/policies")]
[Authorize]
public class PoliciesController : ControllerBase
{
    private readonly AppDbContext _db;
    public PoliciesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] string? q = null,
        [FromQuery] string? policyNo = null,
        [FromQuery] string? vin = null,
        [FromQuery] string? customer = null,
        [FromQuery] string? insurer = null,
        [FromQuery] Guid? vehicleId = null,
        [FromQuery] Guid? partyId = null,
        [FromQuery] bool currentOnly = false)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var query = _db.InsurancePolicies
            .AsNoTracking()
            .Include(x => x.Vehicle)
            .Include(x => x.Party).ThenInclude(x => x.Person)
            .Include(x => x.Party).ThenInclude(x => x.Company)
            .Include(x => x.Insurer)
            .Include(x => x.Template)
            .AsQueryable();

        if (vehicleId is not null) query = query.Where(x => x.VehicleId == vehicleId);
        if (partyId is not null) query = query.Where(x => x.PartyId == partyId);

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            query = query.Where(x =>
                (x.PolicyNo != null && EF.Functions.ILike(x.PolicyNo, $"%{q}%")) ||
                EF.Functions.ILike(x.Vehicle.Vin, $"%{q}%") ||
                EF.Functions.ILike(x.Insurer.Name, $"%{q}%") ||
                (x.Party.Person != null && EF.Functions.ILike(x.Party.Person.FullName, $"%{q}%")) ||
                (x.Party.Company != null && EF.Functions.ILike(x.Party.Company.CompanyName, $"%{q}%")));
        }

        if (!string.IsNullOrWhiteSpace(policyNo))
        {
            policyNo = policyNo.Trim();
            query = query.Where(x => x.PolicyNo != null && EF.Functions.ILike(x.PolicyNo, $"%{policyNo}%"));
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
                (x.Party.Person != null && EF.Functions.ILike(x.Party.Person.FullName, $"%{customer}%")) ||
                (x.Party.Company != null && EF.Functions.ILike(x.Party.Company.CompanyName, $"%{customer}%")));
        }

        if (!string.IsNullOrWhiteSpace(insurer))
        {
            insurer = insurer.Trim();
            query = query.Where(x => EF.Functions.ILike(x.Insurer.Name, $"%{insurer}%"));
        }

        if (currentOnly)
            query = query.Where(x => x.ValidFrom <= today && (x.ValidTo == null || x.ValidTo >= today));

        var items = await query
            .OrderByDescending(x => x.ValidFrom)
            .Select(x => new PolicyDto(
                x.Id,
                x.VehicleId,
                x.Vehicle.Vin,
                ((x.Vehicle.Make ?? "") + " " + (x.Vehicle.Model ?? "")).Trim(),
                x.PartyId,
                x.Party.Person != null ? x.Party.Person.FullName : x.Party.Company != null ? x.Party.Company.CompanyName : "",
                x.InsurerId,
                x.Insurer.Name,
                x.TemplateId,
                x.Template.Name,
                x.PolicyNo,
                x.ValidFrom,
                x.ValidTo,
                x.ValidFrom <= today && (x.ValidTo == null || x.ValidTo >= today),
                x.PremiumAmount,
                x.Currency,
                x.Notes))
            .ToListAsync();

        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> Create(InsurancePolicyUpsertRequest req)
    {
        var entity = new InsurancePolicy
        {
            VisitOperationId = req.VisitOperationId,
            VehicleId = req.VehicleId,
            PartyId = req.PartyId,
            InsurerId = req.InsurerId,
            TemplateId = req.TemplateId,
            PolicyNo = req.PolicyNo,
            ValidFrom = req.ValidFrom,
            ValidTo = req.ValidTo,
            PremiumAmount = req.PremiumAmount,
            Currency = req.Currency,
            Notes = req.Notes
        };
        _db.InsurancePolicies.Add(entity);
        await _db.SaveChangesAsync();
        return Ok(entity);
    }
}

