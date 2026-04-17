using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public static class AppDbSeed
{
    public static async Task SeedAsync(AppDbContext db)
    {
        await SeedVehicleCategoriesAsync(db);
        await SeedInsurersAsync(db);
        await SeedUsersAsync(db);
        await SeedCustomersAsync(db);
        await SeedVehicleDemoDataAsync(db);
    }

    private static async Task SeedVehicleCategoriesAsync(AppDbContext db)
    {
        var categories = new[]
        {
            new VehicleCategory { Code = "M1", Label = "Osebni avtomobil", CategoryGroup = "M", Description = "Motor vehicles for passenger transport" },
            new VehicleCategory { Code = "M2", Label = "Avtobus do 5 t", CategoryGroup = "M", Description = "Passenger transport vehicles" },
            new VehicleCategory { Code = "M3", Label = "Avtobus nad 5 t", CategoryGroup = "M", Description = "Passenger transport vehicles over 5 tonnes" },
            new VehicleCategory { Code = "N1", Label = "Lahko tovorno vozilo", CategoryGroup = "N", Description = "Goods vehicles up to 3.5 tonnes" },
            new VehicleCategory { Code = "N2", Label = "Tovorno vozilo", CategoryGroup = "N", Description = "Goods vehicles 3.5 to 12 tonnes" },
            new VehicleCategory { Code = "N3", Label = "Tezko tovorno vozilo", CategoryGroup = "N", Description = "Goods vehicles over 12 tonnes" },
            new VehicleCategory { Code = "O1", Label = "Priklopnik do 0.75 t", CategoryGroup = "O", Description = "Trailers up to 0.75 tonnes" },
            new VehicleCategory { Code = "O2", Label = "Priklopnik do 3.5 t", CategoryGroup = "O", Description = "Trailers 0.75 to 3.5 tonnes" },
            new VehicleCategory { Code = "L3e", Label = "Motorno kolo", CategoryGroup = "L", Description = "Two-wheeled motorcycle" },
            new VehicleCategory { Code = "L4e", Label = "Motorno kolo s prikolico", CategoryGroup = "L", Description = "Motorcycle with sidecar" },
            new VehicleCategory { Code = "T1", Label = "Traktor", CategoryGroup = "T", Description = "Agricultural tractor" }
        };

        foreach (var category in categories)
        {
            if (!await db.VehicleCategories.AnyAsync(x => x.Code == category.Code))
                db.VehicleCategories.Add(category);
        }

        await db.SaveChangesAsync();
    }

    private static async Task SeedInsurersAsync(AppDbContext db)
    {
        var insurers = new[]
        {
            new Insurer { Code = "TRIGLAV", Name = "Zavarovalnica Triglav d.d." },
            new Insurer { Code = "GENERALI", Name = "Generali zavarovalnica d.d." },
            new Insurer { Code = "GRAWE", Name = "GRAWE zavarovalnica d.d." },
            new Insurer { Code = "SAVA", Name = "Sava zavarovalnica d.d." }
        };

        foreach (var insurer in insurers)
        {
            if (!await db.Insurers.AnyAsync(x => x.Code == insurer.Code))
                db.Insurers.Add(insurer);
        }

        await db.SaveChangesAsync();

        var insurerIds = await db.Insurers.ToDictionaryAsync(x => x.Code, x => x.Id);
        var templates = new[]
        {
            new InsurancePolicyTemplate { InsurerId = insurerIds["TRIGLAV"], Code = "AO", Name = "Obvezno avtomobilsko zavarovanje", Scope = "M1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["TRIGLAV"], Code = "KASKO", Name = "Kasko zavarovanje", Scope = "M1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["GENERALI"], Code = "AO", Name = "Obvezno avtomobilsko zavarovanje", Scope = "M1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["GENERALI"], Code = "KASKO", Name = "Kasko zavarovanje", Scope = "M1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["GRAWE"], Code = "AO", Name = "Obvezno avtomobilsko zavarovanje", Scope = "M1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["GRAWE"], Code = "DELNI_KASKO", Name = "Delni kasko", Scope = "M1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["SAVA"], Code = "AO", Name = "Obvezno avtomobilsko zavarovanje", Scope = "M1" }
        };

        foreach (var template in templates)
        {
            if (!await db.InsurancePolicyTemplates.AnyAsync(x => x.InsurerId == template.InsurerId && x.Code == template.Code))
                db.InsurancePolicyTemplates.Add(template);
        }

        await db.SaveChangesAsync();
    }

    private static async Task SeedUsersAsync(AppDbContext db)
    {
        var users = new[]
        {
            new Users { Username = "admin", DisplayName = "System Admin", Role = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), IsActivated = true },
            new Users { Username = "manager", DisplayName = "Workshop Manager", Role = "manager", PasswordHash = BCrypt.Net.BCrypt.HashPassword("manager123"), IsActivated = true },
            new Users { Username = "employee", DisplayName = "Office Employee", Role = "office", PasswordHash = BCrypt.Net.BCrypt.HashPassword("employee123"), IsActivated = true },
            new Users { Username = "mechanic", DisplayName = "Main Mechanic", Role = "mechanic", PasswordHash = BCrypt.Net.BCrypt.HashPassword("mechanic123"), IsActivated = true }
        };

        foreach (var user in users)
        {
            if (!await db.Users.AnyAsync(x => x.Username == user.Username))
                db.Users.Add(user);
        }

        await db.SaveChangesAsync();
    }

    private static async Task SeedCustomersAsync(AppDbContext db)
    {
        if (await db.Customers.AnyAsync())
            return;

        var customers = new Customers[]
        {
            new()
            {
                Type = "person",
                Address = "Ljubljana",
                Phone = "+386 40 000 000",
                Email = "maja.novak@example.com",
                Person = new People
                {
                    FullName = "Maja Novak",
                    DateOfBirth = new DateOnly(1990, 5, 12),
                    TaxNumber = "12345678",
                    NationalNo = "2505990500000"
                }
            },
            new()
            {
                Type = "person",
                Address = "Celje",
                Phone = "+386 31 222 111",
                Email = "andrej.kovac@example.com",
                Person = new People
                {
                    FullName = "Andrej Kovac",
                    DateOfBirth = new DateOnly(1984, 3, 4),
                    TaxNumber = "22334455",
                    NationalNo = "0403984500123"
                }
            },
            new()
            {
                Type = "person",
                Address = "Kranj",
                Phone = "+386 41 555 888",
                Email = "nina.zupan@example.com",
                Person = new People
                {
                    FullName = "Nina Zupan",
                    DateOfBirth = new DateOnly(1993, 11, 23),
                    TaxNumber = "33445566",
                    NationalNo = "2311993500456"
                }
            },
            new()
            {
                Type = "person",
                Address = "Novo mesto",
                Phone = "+386 30 456 222",
                Email = "borut.vidmar@example.com",
                Person = new People
                {
                    FullName = "Borut Vidmar",
                    DateOfBirth = new DateOnly(1979, 7, 15),
                    TaxNumber = "44556677",
                    NationalNo = "1507979500789"
                }
            },
            new()
            {
                Type = "company",
                Address = "Maribor",
                Phone = "+386 41 111 111",
                Email = "info@avto-doo.si",
                Company = new Companies
                {
                    CompanyName = "Avto d.o.o.",
                    TaxNumber = "87654321",
                    RegistrationNo = "2024/00123"
                }
            },
            new()
            {
                Type = "company",
                Address = "Koper",
                Phone = "+386 51 333 444",
                Email = "fleet@primorje-logistika.si",
                Company = new Companies
                {
                    CompanyName = "Primorje Logistika d.o.o.",
                    TaxNumber = "55443322",
                    RegistrationNo = "2022/00881"
                }
            }
        };

        db.Customers.AddRange(customers);
        await db.SaveChangesAsync();
    }

    private static async Task SeedVehicleDemoDataAsync(AppDbContext db)
    {
        if (await db.Vehicles.AnyAsync())
            return;

        var today = DateOnly.FromDateTime(DateTime.Today);
        var m1CategoryId = await db.VehicleCategories.Where(x => x.Code == "M1").Select(x => x.Id).SingleAsync();
        var officeUserId = await db.Users.Where(x => x.Username == "employee").Select(x => x.Id).SingleAsync();
        var mechanicUserId = await db.Users.Where(x => x.Username == "mechanic").Select(x => x.Id).SingleAsync();

        var customers = await db.Customers
            .Include(x => x.Person)
            .Include(x => x.Company)
            .ToListAsync();

        var customerByName = customers.ToDictionary(GetCustomerDisplayName, x => x);
        var insurerByCode = await db.Insurers.ToDictionaryAsync(x => x.Code, x => x.Id);
        var templateByCode = await db.InsurancePolicyTemplates
            .ToDictionaryAsync(x => $"{x.InsurerId}:{x.Code}", x => x.Id);

        var scenarios = new[]
        {
            new VehicleScenario(
                "VF15RFB0065432101", "Renault", "Clio", 2018, "Maja Novak", "Andrej Kovac",
                "LJ 321 MC", "LJ-CLIO-18", "TRIGLAV", "AO", "AO-CLIO-1801",
                today.AddYears(-3), today.AddMonths(-14), today.AddMonths(-13), today.AddYears(1), "Demo Clio z menjavo lastnika"),
            new VehicleScenario(
                "VF1R9800067812345", "Renault", "Megane", 2020, "Nina Zupan", null,
                "KR 482 NZ", "KR-MEG-20", "GENERALI", "KASKO", "KA-MEG-2007",
                today.AddYears(-2), null, today.AddMonths(-11), today.AddMonths(1), "Renault Megane trenutno v uporabi"),
            new VehicleScenario(
                "SJNFAAJ11U1234501", "Nissan", "Qashqai", 2019, "Avto d.o.o.", null,
                "MB 775 AD", "MB-QAS-19", "SAVA", "AO", "AO-QAS-1944",
                today.AddYears(-2), null, today.AddMonths(-10), today.AddMonths(2), "Sluzbeni Nissan Qashqai"),
            new VehicleScenario(
                "SJNJBAJ10U9988776", "Nissan", "Micra", 2017, "Borut Vidmar", "Maja Novak",
                "NM 114 BV", "NM-MIC-17", "GRAWE", "DELNI_KASKO", "DK-MIC-1722",
                today.AddYears(-4), today.AddYears(-1), today.AddYears(-1), today.AddMonths(5), "Nissan Micra z zgodovino lastnistva"),
            new VehicleScenario(
                "VF15RFB0088800112", "Renault", "Clio", 2021, "Primorje Logistika d.o.o.", null,
                "KP 901 PL", "KP-CLI-21", "TRIGLAV", "KASKO", "KA-CLI-2109",
                today.AddYears(-1), null, today.AddMonths(-5), today.AddMonths(7), "Novejsi Renault Clio za vozni park"),
            new VehicleScenario(
                "VF15RFB0022200456", "Renault", "Clio", 2016, "Andrej Kovac", null,
                "CE 552 AK", "CE-CLI-16", "GENERALI", "AO", "AO-CLI-1611",
                today.AddYears(-5), null, today.AddMonths(-8), today.AddMonths(4), "Starejsi Clio za zgodovinske preglede"),
            new VehicleScenario(
                "VF1BZC0065432123", "Renault", "Captur", 2022, "Nina Zupan", null,
                "KR 777 NZ", "KR-CAP-22", "TRIGLAV", "AO", "AO-CAP-2202",
                today.AddMonths(-10), null, today.AddMonths(-10), today.AddMonths(2), "Renault Captur"),
            new VehicleScenario(
                "SJNTAAJ11U5556677", "Nissan", "X-Trail", 2021, "Primorje Logistika d.o.o.", null,
                "KP 440 XT", "KP-XTR-21", "SAVA", "AO", "AO-XTR-2115",
                today.AddMonths(-18), null, today.AddMonths(-12), today.AddMonths(6), "Nissan X-Trail")
        };

        foreach (var scenario in scenarios)
        {
            await SeedVehicleScenarioAsync(
                db,
                scenario,
                m1CategoryId,
                officeUserId,
                mechanicUserId,
                customerByName,
                insurerByCode,
                templateByCode);
        }

        await db.SaveChangesAsync();
    }

    private static async Task SeedVehicleScenarioAsync(
        AppDbContext db,
        VehicleScenario scenario,
        Guid categoryId,
        Guid officeUserId,
        Guid mechanicUserId,
        IDictionary<string, Customers> customerByName,
        IDictionary<string, Guid> insurerByCode,
        IDictionary<string, Guid> templateByCode)
    {
        var vehicle = new Vehicle
        {
            Vin = scenario.Vin,
            CategoryId = categoryId,
            Make = scenario.Make,
            Model = scenario.Model,
            Year = scenario.Year,
            Notes = scenario.Notes
        };

        db.Vehicles.Add(vehicle);
        await db.SaveChangesAsync();

        var currentOwner = customerByName[scenario.CurrentOwnerName];

        if (scenario.PreviousOwnerName is not null)
        {
            var previousOwner = customerByName[scenario.PreviousOwnerName];
            await AddVisitBundleAsync(
                db,
                previousOwner.Id,
                vehicle.Id,
                officeUserId,
                "ownership_change",
                "Prejsnji lastnik",
                opId =>
                {
                    db.VehicleOwnerships.Add(new VehicleOwnership
                    {
                        VisitOperationId = opId,
                        VehicleId = vehicle.Id,
                        CustomerId = previousOwner.Id,
                        ValidFrom = scenario.FirstOwnershipFrom,
                        ValidTo = scenario.FirstOwnershipTo
                    });
                });
        }

        await AddVisitBundleAsync(
            db,
            currentOwner.Id,
            vehicle.Id,
            officeUserId,
            "ownership_change",
            "Trenutni lastnik",
            opId =>
            {
                db.VehicleOwnerships.Add(new VehicleOwnership
                {
                    VisitOperationId = opId,
                    VehicleId = vehicle.Id,
                    CustomerId = currentOwner.Id,
                    ValidFrom = scenario.CurrentOwnershipFrom
                });
            });

        var plate = new Plate
        {
            PlateNumber = scenario.PlateNumber,
            RegionCode = scenario.PlateNumber.Split(' ')[0],
            Notes = $"Demo tablica za {scenario.Make} {scenario.Model}"
        };

        db.Plates.Add(plate);
        await db.SaveChangesAsync();

        var registration = new VehicleRegistration
        {
            VisitOperationId = Guid.Empty,
            VehicleId = vehicle.Id,
            CustomerId = currentOwner.Id,
            ValidFrom = scenario.RegistrationValidFrom,
            ValidTo = scenario.RegistrationValidTo,
            RegistrationNo = scenario.RegistrationNo,
            Notes = $"Registracija za {scenario.Make} {scenario.Model}"
        };

        await AddVisitBundleAsync(
            db,
            currentOwner.Id,
            vehicle.Id,
            officeUserId,
            "registration_issue",
            "Izdaja registracije",
            opId =>
            {
                registration.VisitOperationId = opId;
                db.VehicleRegistrations.Add(registration);
            });

        await db.SaveChangesAsync();

        await AddVisitBundleAsync(
            db,
            currentOwner.Id,
            vehicle.Id,
            officeUserId,
            "plate_assignment",
            "Dodelitev tablice",
            opId =>
            {
                db.PlateAssignments.Add(new PlateAssignment
                {
                    VisitOperationId = opId,
                    PlateId = plate.Id,
                    RegistrationId = registration.Id,
                    ValidFrom = scenario.RegistrationValidFrom,
                    ValidTo = scenario.RegistrationValidTo
                });
            });

        await AddVisitBundleAsync(
            db,
            currentOwner.Id,
            vehicle.Id,
            officeUserId,
            "insurance_issue",
            "Izdaja police",
            opId =>
            {
                var insurerId = insurerByCode[scenario.InsurerCode];
                var templateId = templateByCode[$"{insurerId}:{scenario.TemplateCode}"];
                db.InsurancePolicies.Add(new InsurancePolicy
                {
                    VisitOperationId = opId,
                    VehicleId = vehicle.Id,
                    CustomerId = currentOwner.Id,
                    InsurerId = insurerId,
                    TemplateId = templateId,
                    PolicyNo = scenario.PolicyNo,
                    ValidFrom = scenario.RegistrationValidFrom,
                    ValidTo = scenario.PolicyValidTo,
                    PremiumAmount = 280m + scenario.Year % 100,
                    Currency = "EUR",
                    Notes = $"Polica za {scenario.Make} {scenario.Model}"
                });
            });

        await AddVisitBundleAsync(
            db,
            currentOwner.Id,
            vehicle.Id,
            mechanicUserId,
            "inspection_finish",
            "Tehnicni pregled",
            opId =>
            {
                db.Inspections.Add(new Inspection
                {
                    VisitOperationId = opId,
                    VehicleId = vehicle.Id,
                    PerformedByUserId = mechanicUserId,
                    Result = "passed",
                    Finished = true,
                    ValidUntil = scenario.PolicyValidTo,
                    OdometerKm = 55000 + scenario.Year * 10,
                    Notes = $"Tehnicni pregled za {scenario.Make} {scenario.Model}"
                });
            });
    }

    private static async Task AddVisitBundleAsync(
        AppDbContext db,
        Guid customerId,
        Guid vehicleId,
        Guid userId,
        string opType,
        string notes,
        Action<Guid> action)
    {
        var visit = new Visit
        {
            CustomerId = customerId,
            VehicleId = vehicleId,
            HandledByUserId = userId,
            Notes = notes
        };

        db.Visits.Add(visit);
        await db.SaveChangesAsync();

        var operation = new VisitOperation
        {
            VisitId = visit.Id,
            OpType = opType,
            Notes = notes
        };

        db.VisitOperations.Add(operation);
        await db.SaveChangesAsync();

        action(operation.Id);
        await db.SaveChangesAsync();
    }

    private static string GetCustomerDisplayName(Customers customer)
        => customer.Person?.FullName ?? customer.Company?.CompanyName ?? customer.Id.ToString();

    private sealed record VehicleScenario(
        string Vin,
        string Make,
        string Model,
        int Year,
        string CurrentOwnerName,
        string? PreviousOwnerName,
        string PlateNumber,
        string RegistrationNo,
        string InsurerCode,
        string TemplateCode,
        string PolicyNo,
        DateOnly FirstOwnershipFrom,
        DateOnly? FirstOwnershipTo,
        DateOnly CurrentOwnershipFrom,
        DateOnly PolicyValidTo,
        string Notes)
    {
        public DateOnly RegistrationValidFrom => CurrentOwnershipFrom;
        public DateOnly? RegistrationValidTo => PolicyValidTo;
    }
}
