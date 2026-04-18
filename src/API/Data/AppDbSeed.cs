using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public static class AppDbSeed
{
    private const int ExpectedPartySeedCount = 44;
    private const int ExpectedVehicleSeedCount = 119;

    public static async Task SeedAsync(AppDbContext db)
    {
        await SeedVehicleCategoriesAsync(db);
        await SeedInsurersAsync(db);
        await SeedUsersAsync(db);
        await ResetIncompleteDemoDataAsync(db);
        await SeedPartiesAsync(db);
        await SeedVehicleDemoDataAsync(db);
    }

    private static async Task ResetIncompleteDemoDataAsync(AppDbContext db)
    {
        var partyCount = await db.Parties.CountAsync();
        var vehicleCount = await db.Vehicles.CountAsync();

        var hasPartialParties = partyCount > 0 && partyCount < ExpectedPartySeedCount;
        var hasPartialVehicles = vehicleCount > 0 && vehicleCount < ExpectedVehicleSeedCount;

        if (!hasPartialParties && !hasPartialVehicles)
            return;

        await db.Database.ExecuteSqlRawAsync("""
            TRUNCATE TABLE
                tehnicni.audit_log,
                tehnicni.homologations,
                tehnicni.inspections,
                tehnicni.insurance_policies,
                tehnicni.plate_assignments,
                tehnicni.plates,
                tehnicni.vehicle_registrations,
                tehnicni.vehicle_ownerships,
                tehnicni.visit_operations,
                tehnicni.visits,
                tehnicni.vehicles,
                tehnicni.companies,
                tehnicni.people,
                tehnicni.parties,
                tehnicni.refresh_tokens,
                tehnicni.user_session_events,
                tehnicni.user_sessions
            RESTART IDENTITY CASCADE;
            """);
    }

    private static async Task SeedVehicleCategoriesAsync(AppDbContext db)
    {
        var categories = new[]
        {
            new VehicleCategory { Code = "L1e", Label = "Moped", CategoryGroup = "L", Description = "Dvokolesno vozilo z manjso prostornino" },
            new VehicleCategory { Code = "L3e", Label = "Motorno kolo", CategoryGroup = "L", Description = "Dvokolesno motorno kolo" },
            new VehicleCategory { Code = "M1", Label = "Osebni avtomobil", CategoryGroup = "M", Description = "Motorna vozila za prevoz potnikov" },
            new VehicleCategory { Code = "M2", Label = "Avtobus do 5 t", CategoryGroup = "M", Description = "Vozila za prevoz potnikov do 5 t" },
            new VehicleCategory { Code = "M3", Label = "Avtobus nad 5 t", CategoryGroup = "M", Description = "Vozila za prevoz potnikov nad 5 t" },
            new VehicleCategory { Code = "N1", Label = "Lahko tovorno vozilo", CategoryGroup = "N", Description = "Vozila za prevoz blaga do 3,5 t" },
            new VehicleCategory { Code = "N2", Label = "Tovorno vozilo", CategoryGroup = "N", Description = "Vozila za prevoz blaga od 3,5 t do 12 t" },
            new VehicleCategory { Code = "N3", Label = "Tezko tovorno vozilo", CategoryGroup = "N", Description = "Vozila za prevoz blaga nad 12 t" },
            new VehicleCategory { Code = "O1", Label = "Priklopnik do 0,75 t", CategoryGroup = "O", Description = "Priklopniki do 0,75 t" },
            new VehicleCategory { Code = "O2", Label = "Priklopnik do 3,5 t", CategoryGroup = "O", Description = "Priklopniki od 0,75 t do 3,5 t" },
            new VehicleCategory { Code = "T1", Label = "Traktor", CategoryGroup = "T", Description = "Kmetijski traktor" }
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
            new InsurancePolicyTemplate { InsurerId = insurerIds["TRIGLAV"], Code = "AO", Name = "Obvezno avtomobilsko zavarovanje", Scope = "M1,N1,N2,N3,L1e,L3e,O1,O2,T1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["TRIGLAV"], Code = "KASKO", Name = "Kasko zavarovanje", Scope = "M1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["GENERALI"], Code = "AO", Name = "Obvezno avtomobilsko zavarovanje", Scope = "M1,N1,N2,N3,L1e,L3e,O1,O2,T1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["GENERALI"], Code = "KASKO", Name = "Kasko zavarovanje", Scope = "M1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["GRAWE"], Code = "AO", Name = "Obvezno avtomobilsko zavarovanje", Scope = "M1,N1,N2,N3,L1e,L3e,O1,O2,T1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["GRAWE"], Code = "DELNI_KASKO", Name = "Delni kasko", Scope = "M1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["SAVA"], Code = "AO", Name = "Obvezno avtomobilsko zavarovanje", Scope = "M1,N1,N2,N3,L1e,L3e,O1,O2,T1" },
            new InsurancePolicyTemplate { InsurerId = insurerIds["SAVA"], Code = "KASKO", Name = "Kasko zavarovanje", Scope = "M1" }
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
            new Users { Username = "manager", DisplayName = "Vodja poslovalnice", Role = "manager", PasswordHash = BCrypt.Net.BCrypt.HashPassword("manager123"), IsActivated = true },
            new Users { Username = "employee", DisplayName = "Administrativni referent", Role = "office", PasswordHash = BCrypt.Net.BCrypt.HashPassword("employee123"), IsActivated = true },
            new Users { Username = "mechanic", DisplayName = "Glavni mehanik", Role = "mechanic", PasswordHash = BCrypt.Net.BCrypt.HashPassword("mechanic123"), IsActivated = true }
        };

        foreach (var user in users)
        {
            if (!await db.Users.AnyAsync(x => x.Username == user.Username))
                db.Users.Add(user);
        }

        await db.SaveChangesAsync();
    }

    private static async Task SeedPartiesAsync(AppDbContext db)
    {
        if (await db.Parties.AnyAsync())
            return;

        var parties = new List<Party>
        {
            CreatePerson("Maja Novak", "Dunajska cesta 14, 1000 Ljubljana", "+386 40 000 110", "maja.novak@example.com", new DateOnly(1990, 5, 12), "12345678", true, 1),
            CreatePerson("Andrej Kovac", "Kidriceva ulica 9, 3000 Celje", "+386 31 222 111", "andrej.kovac@example.com", new DateOnly(1984, 3, 4), "22334455", false, 1),
            CreatePerson("Nina Zupan", "Bleiweisova cesta 27, 4000 Kranj", "+386 41 555 888", "nina.zupan@example.com", new DateOnly(1993, 11, 23), "33445566", true, 2),
            CreatePerson("Borut Vidmar", "Ljubljanska cesta 48, 8000 Novo mesto", "+386 30 456 222", "borut.vidmar@example.com", new DateOnly(1979, 7, 15), "44556677", false, 2),
            CreatePerson("Petra Mlakar", "Cesta na Brdo 33, 1000 Ljubljana", "+386 40 812 411", "petra.mlakar@example.com", new DateOnly(1988, 9, 3), "55667788", true, 3),
            CreatePerson("Luka Kranjc", "Trg svobode 6, 2000 Maribor", "+386 41 812 512", "luka.kranjc@example.com", new DateOnly(1987, 4, 21), "66778899", false, 3),
            CreatePerson("Tjasa Horvat", "Vojkova cesta 72, 1000 Ljubljana", "+386 51 223 144", "tjasa.horvat@example.com", new DateOnly(1995, 2, 10), "77889911", true, 4),
            CreatePerson("Miha Potocnik", "Delpinova ulica 12, 5000 Nova Gorica", "+386 31 744 223", "miha.potocnik@example.com", new DateOnly(1982, 12, 1), "88991122", false, 4),
            CreatePerson("Eva Bizjak", "Obala 114, 6320 Portoroz", "+386 40 221 643", "eva.bizjak@example.com", new DateOnly(1991, 6, 17), "99112233", true, 5),
            CreatePerson("Jure Kos", "Partizanska cesta 18, 2000 Maribor", "+386 41 451 221", "jure.kos@example.com", new DateOnly(1985, 1, 26), "11223344", false, 5),
            CreatePerson("Sara Zajc", "Gubceva ulica 7, 1230 Domzale", "+386 31 611 120", "sara.zajc@example.com", new DateOnly(1994, 8, 30), "22335577", true, 6),
            CreatePerson("Rok Tratnik", "Saleska cesta 21, 3320 Velenje", "+386 40 155 732", "rok.tratnik@example.com", new DateOnly(1989, 10, 11), "33446688", false, 6),
            CreatePerson("Anja Oblak", "Cankarjeva cesta 41, 6000 Koper", "+386 41 774 305", "anja.oblak@example.com", new DateOnly(1996, 3, 2), "44557799", true, 7),
            CreatePerson("Gregor Sever", "Koroska cesta 55, 4000 Kranj", "+386 31 522 610", "gregor.sever@example.com", new DateOnly(1986, 7, 9), "55668811", false, 7),
            CreatePerson("Mojca Pirc", "Trubarjeva cesta 29, 1290 Grosuplje", "+386 40 923 117", "mojca.pirc@example.com", new DateOnly(1992, 11, 5), "66779922", true, 8),
            CreatePerson("Zan Kovacic", "Mariborska cesta 83, 3000 Celje", "+386 41 901 544", "zan.kovacic@example.com", new DateOnly(1998, 1, 13), "77881133", false, 8),
            CreatePerson("Urska Kralj", "Presernova cesta 12, 1000 Ljubljana", "+386 31 455 700", "urska.kralj@example.com", new DateOnly(1983, 5, 28), "88992244", true, 9),
            CreatePerson("Matjaz Turk", "Cesta v Mestni log 77, 1000 Ljubljana", "+386 40 611 801", "matjaz.turk@example.com", new DateOnly(1978, 2, 19), "99113355", false, 9),
            CreatePerson("Klara Rozman", "Askerceva cesta 16, 1000 Ljubljana", "+386 31 700 101", "klara.rozman@example.com", new DateOnly(1986, 6, 24), "11447722", true, 10),
            CreatePerson("Marko Hribar", "Cesta na Loko 8, 4290 Trzic", "+386 41 700 102", "marko.hribar@example.com", new DateOnly(1981, 9, 14), "22558833", false, 10),
            CreatePerson("Nejc Vidic", "Kersnikova ulica 25, 1000 Ljubljana", "+386 40 700 103", "nejc.vidic@example.com", new DateOnly(1997, 4, 8), "33669944", false, 11),
            CreatePerson("Lana Tomazic", "Pot na Lisice 4, 6310 Izola", "+386 31 700 104", "lana.tomazic@example.com", new DateOnly(1999, 7, 20), "44771155", true, 11),
            CreatePerson("Ales Jerman", "Lendavska ulica 12, 9000 Murska Sobota", "+386 41 700 105", "ales.jerman@example.com", new DateOnly(1977, 10, 6), "55882266", false, 12),
            CreatePerson("Nika Smodis", "Kosovelova ulica 31, 8000 Novo mesto", "+386 40 700 106", "nika.smodis@example.com", new DateOnly(1993, 3, 19), "66993377", true, 12),
            CreatePerson("Urban Ceh", "Ulica Staneta Rozmana 11, 2250 Ptuj", "+386 31 700 107", "urban.ceh@example.com", new DateOnly(1988, 12, 2), "77114455", false, 13),
            CreatePerson("Manca Lavric", "Levstikova ulica 9, 1234 Menges", "+386 41 700 108", "manca.lavric@example.com", new DateOnly(1995, 1, 29), "88225566", true, 13),

            CreateCompany("Intereuropa, d.d.", "Vojkovo nabrezje 32, 6000 Koper", "+386 5 665 10 00", "info@intereuropa.si", "55443322", "82456610"),
            CreateCompany("Porsche Inter Auto, d.o.o.", "Ptujska cesta 117, 2000 Maribor", "+386 2 450 26 00", "info@porscheinterauto.net", "87654321", "61324570"),
            CreateCompany("Posta Slovenije d.o.o.", "Slomskov trg 10, 2000 Maribor", "+386 2 449 26 00", "info@posta.si", "41239876", "90125530"),
            CreateCompany("DARS d.d.", "Ulica XIV. divizije 4, 3000 Celje", "+386 3 426 45 00", "info@dars.si", "53981124", "74125560"),
            CreateCompany("Luka Koper, d.d.", "Vojkovo nabrezje 38, 6000 Koper", "+386 5 665 61 00", "info@luka-kp.si", "66442211", "55224510"),
            CreateCompany("Petrol d.d., Ljubljana", "Dunajska cesta 50, 1000 Ljubljana", "+386 1 471 46 00", "info@petrol.si", "77114422", "32884410"),
            CreateCompany("Elektro Ljubljana d.d.", "Slovenska cesta 58, 1516 Ljubljana", "+386 1 230 40 00", "info@elektro-ljubljana.si", "88116633", "44228810"),
            CreateCompany("Kolektor CPG d.o.o.", "Industrijska cesta 2, 5000 Nova Gorica", "+386 5 339 21 00", "info@kolektorcpg.si", "99337744", "67225510"),
            CreateCompany("Nomago d.o.o.", "Vojkova cesta 100, 1000 Ljubljana", "+386 1 431 77 00", "info@nomago.si", "31445566", "51442290"),
            CreateCompany("Telekom Slovenije, d.d.", "Cigaletova ulica 15, 1000 Ljubljana", "+386 1 234 10 00", "info@telekom.si", "42556677", "63881124"),
            CreateCompany("Avto Celje d.o.o.", "Mariborska cesta 53, 3000 Celje", "+386 3 425 62 00", "info@avtocelje.si", "53667788", "45224411"),
            CreateCompany("AMZS d.d.", "Dunajska cesta 128a, 1000 Ljubljana", "+386 1 530 53 00", "info@amzs.si", "64778899", "56335522"),
            CreateCompany("Slovenske zeleznice, d.o.o.", "Kolodvorska ulica 11, 1000 Ljubljana", "+386 1 291 21 00", "info@slo-zeleznice.si", "75889911", "67446633"),
            CreateCompany("Policija", "Stefanova ulica 2, 1000 Ljubljana", "+386 1 428 40 00", "gp.policija@policija.si", "86991122", "78557744"),
            CreateCompany("Slovenska vojska", "Vojkova cesta 55, 1000 Ljubljana", "+386 1 471 22 11", "glavna.pisarna@mors.si", "97112233", "89668855"),
            CreateCompany("Veleposlanistvo Zdruzenih drzav Amerike v Ljubljani", "Presernova cesta 31, 1000 Ljubljana", "+386 1 200 55 00", "ljubljanaacs@state.gov", "18223344", "11779966"),
            CreateCompany("Panvita kmetijstvo d.o.o.", "Lendavska ulica 5, 9000 Murska Sobota", "+386 2 530 16 00", "info@panvita.si", "29334455", "22881177"),
            CreateCompany("TPV d.o.o.", "Kandijska cesta 60, 8000 Novo mesto", "+386 7 393 22 00", "info@tpv.si", "30445566", "33992288")
        };

        db.Parties.AddRange(parties);
        await db.SaveChangesAsync();
    }

    private static async Task SeedVehicleDemoDataAsync(AppDbContext db)
    {
        if (await db.Vehicles.AnyAsync())
            return;

        var today = DateOnly.FromDateTime(DateTime.Today);
        var categoryIds = await db.VehicleCategories.ToDictionaryAsync(x => x.Code, x => x.Id);
        var officeUserId = await db.Users.Where(x => x.Username == "employee").Select(x => x.Id).SingleAsync();
        var mechanicUserId = await db.Users.Where(x => x.Username == "mechanic").Select(x => x.Id).SingleAsync();

        var parties = await db.Parties
            .Include(x => x.Person)
            .Include(x => x.Company)
            .ToListAsync();

        var partyByName = parties.ToDictionary(GetPartyDisplayName, x => x);
        var insurerByCode = await db.Insurers.ToDictionaryAsync(x => x.Code, x => x.Id);
        var templateByKey = await db.InsurancePolicyTemplates.ToDictionaryAsync(x => $"{x.InsurerId}:{x.Code}", x => x.Id);

        var vehicleSeeds = new List<VehicleSeedSpec>
        {
            new("Renault", "Clio", 2018, "M1", "Maja Novak", "LJ", "TRIGLAV", "AO", 14, 10, "Andrej Kovac", 42, 24),
            new("Renault", "Clio", 2021, "M1", "Intereuropa, d.d.", "KP", "TRIGLAV", "KASKO", 20, 8, null, null, 18),
            new("Renault", "Clio", 2016, "M1", "Andrej Kovac", "CE", "GENERALI", "AO", 30, 7, null, null, 19),
            new("Renault", "Captur", 2022, "M1", "Nina Zupan", "KR", "TRIGLAV", "KASKO", 16, 9),
            new("Renault", "Megane", 2020, "M1", "Petra Mlakar", "LJ", "GENERALI", "AO", 13, 11, "Luka Kranjc", 38, 22),
            new("Renault", "Kadjar", 2019, "M1", "Gregor Sever", "KR", "SAVA", "AO", 21, 6),
            new("Renault", "Trafic", 2021, "N1", "Intereuropa, d.d.", "KP", "TRIGLAV", "AO", 24, 7, null, null, 17),
            new("Renault", "Clio", 2023, "M1", "Porsche Inter Auto, d.o.o.", "MB", "TRIGLAV", "KASKO", 8, 5),
            new("Nissan", "Qashqai", 2019, "M1", "Porsche Inter Auto, d.o.o.", "MB", "SAVA", "AO", 18, 10),
            new("Nissan", "Micra", 2017, "M1", "Borut Vidmar", "NM", "GRAWE", "DELNI_KASKO", 12, 10, "Maja Novak", 46, 25),
            new("Nissan", "X-Trail", 2021, "M1", "Intereuropa, d.d.", "KP", "SAVA", "AO", 15, 9),
            new("Nissan", "Juke", 2022, "M1", "Anja Oblak", "KP", "GENERALI", "KASKO", 11, 7),
            new("Nissan", "Navara", 2020, "N1", "Petrol d.d., Ljubljana", "CE", "TRIGLAV", "AO", 26, 8),
            new("Nissan", "Leaf", 2023, "M1", "Tjasa Horvat", "LJ", "GENERALI", "AO", 7, 6),
            new("Peugeot", "208", 2019, "M1", "Eva Bizjak", "KP", "TRIGLAV", "AO", 17, 10, null, null, 23),
            new("Peugeot", "308", 2021, "M1", "Luka Kranjc", "MB", "GENERALI", "KASKO", 15, 8),
            new("Peugeot", "3008", 2022, "M1", "Luka Koper, d.d.", "KP", "TRIGLAV", "KASKO", 12, 7),
            new("Peugeot", "Partner", 2020, "N1", "Posta Slovenije d.o.o.", "LJ", "SAVA", "AO", 20, 8),
            new("Citroen", "C3", 2018, "M1", "Sara Zajc", "LJ", "GENERALI", "AO", 22, 9),
            new("Citroen", "C4", 2023, "M1", "Zan Kovacic", "CE", "TRIGLAV", "AO", 6, 5),
            new("Citroen", "Berlingo", 2021, "N1", "DARS d.d.", "KR", "GRAWE", "AO", 16, 9),
            new("DS", "4", 2022, "M1", "Petra Mlakar", "LJ", "SAVA", "KASKO", 8, 6),
            new("Volkswagen", "Golf", 2020, "M1", "Maja Novak", "LJ", "TRIGLAV", "KASKO", 19, 7, null, null, 19),
            new("Volkswagen", "Passat", 2019, "M1", "Jure Kos", "MB", "GENERALI", "AO", 21, 10),
            new("Volkswagen", "Tiguan", 2022, "M1", "Kolektor CPG d.o.o.", "PO", "TRIGLAV", "KASKO", 10, 7),
            new("Volkswagen", "Caddy", 2021, "N1", "DARS d.d.", "KR", "SAVA", "AO", 15, 8),
            new("Audi", "A3", 2018, "M1", "Gregor Sever", "KR", "GENERALI", "AO", 13, 9, "Eva Bizjak", 40, 24),
            new("Audi", "A4", 2021, "M1", "Miha Potocnik", "GO", "TRIGLAV", "KASKO", 14, 8),
            new("BMW", "320d", 2020, "M1", "Borut Vidmar", "NM", "SAVA", "KASKO", 18, 9),
            new("BMW", "X1", 2022, "M1", "Nina Zupan", "KR", "GENERALI", "KASKO", 9, 7),
            new("Mercedes-Benz", "A 180", 2021, "M1", "Mojca Pirc", "LJ", "TRIGLAV", "KASKO", 15, 8),
            new("Mercedes-Benz", "Vito", 2020, "N1", "Nomago d.o.o.", "CE", "SAVA", "AO", 20, 9),
            new("Opel", "Astra", 2019, "M1", "Elektro Ljubljana d.d.", "GO", "GENERALI", "AO", 17, 10),
            new("Opel", "Corsa", 2022, "M1", "Eva Bizjak", "KP", "TRIGLAV", "AO", 10, 7),
            new("Fiat", "500", 2018, "M1", "Sara Zajc", "LJ", "GRAWE", "AO", 12, 9, "Petra Mlakar", 34, 21),
            new("Fiat", "Tipo", 2021, "M1", "Zan Kovacic", "CE", "GENERALI", "AO", 13, 8),
            new("Fiat", "Ducato", 2020, "N1", "Posta Slovenije d.o.o.", "LJ", "TRIGLAV", "AO", 24, 9),
            new("Alfa Romeo", "Tonale", 2023, "M1", "Luka Kranjc", "MB", "TRIGLAV", "KASKO", 6, 5),
            new("Ford", "Focus", 2020, "M1", "Andrej Kovac", "CE", "GENERALI", "AO", 16, 9),
            new("Tesla", "Model 3", 2023, "M1", "Nina Zupan", "KR", "TRIGLAV", "KASKO", 5, 5)
        };

        vehicleSeeds.AddRange(BuildExpandedVehicleSeeds());

        for (var index = 0; index < vehicleSeeds.Count; index++)
        {
            await SeedVehicleScenarioAsync(
                db,
                vehicleSeeds[index],
                index + 1,
                categoryIds,
                officeUserId,
                mechanicUserId,
                partyByName,
                insurerByCode,
                templateByKey,
                today);
        }
    }

    private static async Task SeedVehicleScenarioAsync(
        AppDbContext db,
        VehicleSeedSpec seed,
        int sequence,
        IReadOnlyDictionary<string, Guid> categoryIds,
        Guid officeUserId,
        Guid mechanicUserId,
        IReadOnlyDictionary<string, Party> partyByName,
        IReadOnlyDictionary<string, Guid> insurerByCode,
        IReadOnlyDictionary<string, Guid> templateByKey,
        DateOnly today)
    {
        var currentParty = partyByName[seed.CurrentPartyName];
        var currentOwnershipFrom = today.AddMonths(-seed.CurrentOwnershipMonthsAgo);
        var currentRegistrationFrom = today.AddMonths(-seed.CurrentRegistrationMonthsAgo);
        var currentRegistrationValidTo = currentRegistrationFrom.AddYears(1).AddDays(-1);

        var vehicle = new Vehicle
        {
            Vin = BuildVin(seed.Make, seed.Model, seed.Year, sequence),
            CategoryId = categoryIds[seed.CategoryCode],
            Make = seed.Make,
            Model = seed.Model,
            Year = seed.Year,
            Notes = $"{seed.Make} {seed.Model} ({seed.CategoryCode}) - demo zapis"
        };

        db.Vehicles.Add(vehicle);
        await db.SaveChangesAsync();

        if (!string.IsNullOrWhiteSpace(seed.PreviousPartyName) && seed.PreviousOwnershipMonthsAgo is not null)
        {
            var previousParty = partyByName[seed.PreviousPartyName];
            var previousOwnershipFrom = today.AddMonths(-seed.PreviousOwnershipMonthsAgo.Value);
            var previousOwnershipTo = currentOwnershipFrom.AddDays(-1);

            var previousOwnershipVisitId = await CreateVisitAsync(db, previousParty.Id, vehicle.Id, officeUserId, "Prejsnje lastnistvo");
            var previousOwnershipOperationId = await AddOperationAsync(db, previousOwnershipVisitId, "ownership_change", "Prejsnji lastnik");
            db.VehicleOwnerships.Add(new VehicleOwnership
            {
                VisitOperationId = previousOwnershipOperationId,
                VehicleId = vehicle.Id,
                PartyId = previousParty.Id,
                ValidFrom = previousOwnershipFrom,
                ValidTo = previousOwnershipTo
            });
            await db.SaveChangesAsync();

            if (seed.PreviousRegistrationMonthsAgo is not null)
            {
                await AddRegistrationBundleAsync(
                    db,
                    previousParty.Id,
                    vehicle.Id,
                    officeUserId,
                    mechanicUserId,
                    insurerByCode,
                    templateByKey,
                    seed,
                    sequence + 400,
                    today.AddMonths(-seed.PreviousRegistrationMonthsAgo.Value),
                    currentRegistrationFrom.AddDays(-1),
                    false);
            }
        }
        else if (seed.PreviousRegistrationMonthsAgo is not null)
        {
            await AddRegistrationBundleAsync(
                db,
                currentParty.Id,
                vehicle.Id,
                officeUserId,
                mechanicUserId,
                insurerByCode,
                templateByKey,
                seed,
                sequence + 200,
                today.AddMonths(-seed.PreviousRegistrationMonthsAgo.Value),
                currentRegistrationFrom.AddDays(-1),
                false);
        }

        var currentOwnershipVisitId = await CreateVisitAsync(db, currentParty.Id, vehicle.Id, officeUserId, "Trenutno lastnistvo");
        var currentOwnershipOperationId = await AddOperationAsync(db, currentOwnershipVisitId, "ownership_change", "Trenutni lastnik");
        db.VehicleOwnerships.Add(new VehicleOwnership
        {
            VisitOperationId = currentOwnershipOperationId,
            VehicleId = vehicle.Id,
            PartyId = currentParty.Id,
            ValidFrom = currentOwnershipFrom,
            ValidTo = null
        });
        await db.SaveChangesAsync();

        await AddRegistrationBundleAsync(
            db,
            currentParty.Id,
            vehicle.Id,
            officeUserId,
            mechanicUserId,
            insurerByCode,
            templateByKey,
            seed,
            sequence,
            currentRegistrationFrom,
            currentRegistrationValidTo,
            true);
    }

    private static async Task AddRegistrationBundleAsync(
        AppDbContext db,
        Guid partyId,
        Guid vehicleId,
        Guid officeUserId,
        Guid mechanicUserId,
        IReadOnlyDictionary<string, Guid> insurerByCode,
        IReadOnlyDictionary<string, Guid> templateByKey,
        VehicleSeedSpec seed,
        int sequence,
        DateOnly validFrom,
        DateOnly validTo,
        bool isCurrent)
    {
        var visitId = await CreateVisitAsync(
            db,
            partyId,
            vehicleId,
            officeUserId,
            isCurrent ? "Registracija, polica in tehnicni pregled" : "Pretekla registracija");

        var registrationOperationId = await AddOperationAsync(
            db,
            visitId,
            "registration_issue",
            isCurrent ? "Izdaja trenutne registracije" : "Izdaja pretekle registracije");

        var registration = new VehicleRegistration
        {
            VisitOperationId = registrationOperationId,
            VehicleId = vehicleId,
            PartyId = partyId,
            RegistrationNo = seed.RegistrationNoOverride ?? BuildRegistrationNo(validFrom.Year, sequence),
            ValidFrom = validFrom,
            ValidTo = validTo,
            Notes = $"{seed.Make} {seed.Model} - {(isCurrent ? "trenutna" : "pretekla")} registracija"
        };
        db.VehicleRegistrations.Add(registration);
        await db.SaveChangesAsync();

        var plate = new Plate
        {
            PlateNo = seed.PlateNoOverride ?? BuildPlateNo(seed.PlatePrefix, sequence, seed.PlateType),
            PlateType = seed.PlateType
        };
        db.Plates.Add(plate);
        await db.SaveChangesAsync();

        var plateAssignmentOperationId = await AddOperationAsync(
            db,
            visitId,
            "plate_assignment",
            isCurrent ? "Dodelitev trenutne tablice" : "Dodelitev pretekle tablice");

        db.PlateAssignments.Add(new PlateAssignment
        {
            VisitOperationId = plateAssignmentOperationId,
            PlateId = plate.Id,
            RegistrationId = registration.Id,
            ValidFrom = validFrom,
            ValidTo = validTo
        });
        await db.SaveChangesAsync();

        var insuranceOperationId = await AddOperationAsync(
            db,
            visitId,
            "insurance_issue",
            isCurrent ? "Izdaja trenutne police" : "Izdaja pretekle police");

        var insurerId = insurerByCode[seed.InsurerCode];
        var templateId = templateByKey[$"{insurerId}:{seed.TemplateCode}"];
        db.InsurancePolicies.Add(new InsurancePolicy
        {
            VisitOperationId = insuranceOperationId,
            VehicleId = vehicleId,
            PartyId = partyId,
            InsurerId = insurerId,
            TemplateId = templateId,
            PolicyNo = BuildPolicyNo(seed.InsurerCode, sequence),
            ValidFrom = validFrom,
            ValidTo = validTo,
            PremiumAmount = CalculatePremium(seed.CategoryCode, seed.Year, isCurrent),
            Currency = "EUR",
            Notes = $"{seed.Make} {seed.Model} - {(isCurrent ? "trenutna" : "pretekla")} polica"
        });
        await db.SaveChangesAsync();

        var inspectionOperationId = await AddOperationAsync(
            db,
            visitId,
            "inspection_finish",
            isCurrent ? "Trenutni tehnicni pregled" : "Pretekli tehnicni pregled");

        db.Inspections.Add(new Inspection
        {
            VisitOperationId = inspectionOperationId,
            VehicleId = vehicleId,
            PerformedByUserId = mechanicUserId,
            PerformedAt = new DateTimeOffset(validFrom.ToDateTime(TimeOnly.MinValue), TimeSpan.Zero),
            Result = "passed",
            Finished = true,
            ValidUntil = validTo,
            OdometerKm = CalculateOdometer(seed.Year, sequence, isCurrent),
            Notes = $"{seed.Make} {seed.Model} - opravljen tehnicni pregled"
        });
        await db.SaveChangesAsync();
    }

    private static async Task<Guid> CreateVisitAsync(AppDbContext db, Guid partyId, Guid vehicleId, Guid userId, string notes)
    {
        var visit = new Visit
        {
            PartyId = partyId,
            VehicleId = vehicleId,
            HandledByUserId = userId,
            Notes = notes
        };

        db.Visits.Add(visit);
        await db.SaveChangesAsync();
        return visit.Id;
    }

    private static async Task<Guid> AddOperationAsync(AppDbContext db, Guid visitId, string opType, string notes)
    {
        var operation = new VisitOperation
        {
            VisitId = visitId,
            OpType = opType,
            Notes = notes
        };

        db.VisitOperations.Add(operation);
        await db.SaveChangesAsync();
        return operation.Id;
    }

    private static Party CreatePerson(
        string fullName,
        string address,
        string phone,
        string email,
        DateOnly dateOfBirth,
        string taxNo,
        bool isFemale,
        int serialNo,
        int registerCode = 50)
        => new()
        {
            Type = "person",
            Address = address,
            Phone = phone,
            Email = email,
            Person = new Person
            {
                FullName = fullName,
                DateOfBirth = dateOfBirth,
                TaxNo = taxNo,
                Emso = BuildEmso(dateOfBirth, registerCode, serialNo, isFemale)
            }
        };

    private static Party CreateCompany(
        string companyName,
        string address,
        string phone,
        string email,
        string taxNo,
        string companyRegNo)
        => new()
        {
            Type = "company",
            Address = address,
            Phone = phone,
            Email = email,
            Company = new Company
            {
                CompanyName = companyName,
                TaxNo = taxNo,
                CompanyRegNo = companyRegNo
            }
        };

    private static string GetPartyDisplayName(Party party)
        => party.Person?.FullName ?? party.Company?.CompanyName ?? party.Id.ToString();

    private static string BuildEmso(DateOnly dateOfBirth, int registerCode, int serialNo, bool isFemale)
    {
        var baseSerial = Math.Clamp(serialNo, 1, 499);
        var candidateStart = (isFemale ? 500 : 0) + baseSerial;

        for (var candidate = candidateStart; candidate <= (isFemale ? 999 : 499); candidate++)
        {
            var prefix = $"{dateOfBirth.Day:00}{dateOfBirth.Month:00}{dateOfBirth.Year % 1000:000}{registerCode:00}{candidate:000}";
            var controlDigit = CalculateEmsoControlDigit(prefix);
            if (controlDigit is not null)
                return $"{prefix}{controlDigit.Value}";
        }

        throw new InvalidOperationException($"Ni mogoce sestaviti veljavnega EMSO za datum {dateOfBirth:dd.MM.yyyy}.");
    }

    private static int? CalculateEmsoControlDigit(string firstTwelveDigits)
    {
        var weights = new[] { 7, 6, 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
        var sum = 0;

        for (var i = 0; i < weights.Length; i++)
            sum += (firstTwelveDigits[i] - '0') * weights[i];

        var control = 11 - (sum % 11);
        return control switch
        {
            11 => 0,
            10 => null,
            _ => control
        };
    }

    private static string BuildVin(string make, string model, int year, int sequence)
    {
        var prefix = make switch
        {
            "Renault" => "VF1",
            "Nissan" => "SJN",
            "Peugeot" => "VF3",
            "Citroen" => "VF7",
            "DS" => "VR1",
            "Volkswagen" => "WVW",
            "Audi" => "WAU",
            "BMW" when model.StartsWith("R ", StringComparison.OrdinalIgnoreCase) => "WB1",
            "BMW" => "WBA",
            "Mercedes-Benz" => "WDD",
            "Opel" => "W0L",
            "Fiat" => "ZFA",
            "Alfa Romeo" => "ZAR",
            "Ford" => "WF0",
            "Jeep" => "ZAC",
            "Tesla" => "LRW",
            "Skoda" => "TMB",
            "Seat" => "VSS",
            "Dacia" => "UU1",
            "Toyota" => "JTD",
            "Kia" => "U5Y",
            "Hyundai" => "TMA",
            "Volvo" => "YV1",
            "MAN" => "WMA",
            "Piaggio" => "ZAP",
            "Tomos" => "SDL",
            "New Holland" => "HAC",
            "Boeckmann" => "WHB",
            _ => "ZZZ"
        };

        var modelToken = new string($"{make}{model}{year % 100:D2}"
            .ToUpperInvariant()
            .Where(char.IsLetterOrDigit)
            .Select(ch => ch switch { 'I' => 'X', 'O' => 'X', 'Q' => 'X', _ => ch })
            .Take(8)
            .ToArray());

        modelToken = modelToken.PadRight(8, 'X');
        return $"{prefix}{modelToken}{sequence:D6}";
    }

    private static string BuildPlateNo(string platePrefix, int sequence, string plateType)
    {
        const string letters = "ABCDEFGHJKLMNPRSTUVZ";
        var first = letters[(sequence / letters.Length) % letters.Length];
        var second = letters[sequence % letters.Length];
        var third = letters[(sequence * 3) % letters.Length];
        var twoDigits = 10 + (sequence % 90);
        var threeDigits = 100 + (sequence % 900);

        if (plateType == "standard")
        {
            return (sequence % 4) switch
            {
                0 => $"{platePrefix} {first}{second}-{threeDigits:D3}",
                1 => $"{platePrefix} {first}{sequence % 10}-{200 + (sequence % 800):D3}",
                2 => $"{platePrefix} {first}{second}-{twoDigits:D2}{third}",
                _ => $"{platePrefix} {twoDigits:D2}-{first}{second}{third}"
            };
        }

        return plateType switch
        {
            "police" => $"P {sequence % 90:00}-{threeDigits:D3}",
            "military" => $"SV {sequence % 90:00}-{threeDigits:D3}",
            "diplomatic" => $"CD {10 + (sequence % 80):00}-{10 + ((sequence * 3) % 80):00}",
            "test" => $"PR {sequence % 90:00}-{threeDigits:D3}",
            "temporary" => $"{platePrefix} PR-{threeDigits:D3}",
            "export" => $"EX {sequence % 90:00}-{threeDigits:D3}",
            "agricultural" => $"{platePrefix} {first}{second}-{twoDigits:D2}",
            "trailer" => $"{threeDigits:D3} {first}{second}-{platePrefix}",
            "moped" => $"{platePrefix} {sequence % 9}{first}-{twoDigits:D2}",
            _ => $"{platePrefix} {first}{second}-{threeDigits:D3}"
        };
    }

    private static IReadOnlyList<VehicleSeedSpec> BuildExpandedVehicleSeeds()
    {
        var seeds = new List<VehicleSeedSpec>
        {
            new("Skoda", "Octavia Combi 2.0 TDI", 2022, "M1", "Policija", "LJ", "TRIGLAV", "AO", 4, 4,
                PlateType: "police", PlateNoOverride: "P 01-231"),
            new("Renault", "Kadjar Intens Blue", 2019, "M1", "Policija", "LJ", "GENERALI", "AO", 8, 6,
                PlateType: "police", PlateNoOverride: "P 02-417"),
            new("Renault", "Megane Grandtour", 2019, "M1", "Policija", "LJ", "TRIGLAV", "AO", 7, 5,
                PlateType: "police", PlateNoOverride: "P 03-522"),
            new("Renault", "Megane 1.5 dCi", 2017, "M1", "Slovenska vojska", "LJ", "SAVA", "AO", 18, 12,
                PlateType: "military", PlateNoOverride: "SV 11-245"),
            new("Mercedes-Benz", "G270 CDI", 2016, "M1", "Slovenska vojska", "LJ", "TRIGLAV", "AO", 24, 12,
                PlateType: "military", PlateNoOverride: "SV 12-312"),
            new("Volkswagen", "Transporter T6", 2020, "N1", "Slovenska vojska", "LJ", "GENERALI", "AO", 20, 11,
                PlateType: "military", PlateNoOverride: "SV 13-115"),
            new("MAN", "15TMILGL W A1", 2022, "N3", "Slovenska vojska", "LJ", "SAVA", "AO", 10, 10,
                PlateType: "military", PlateNoOverride: "SV 20-801"),
            new("Ford", "Mondeo Hybrid", 2020, "M1", "Veleposlanistvo Zdruzenih drzav Amerike v Ljubljani", "LJ", "GRAWE", "AO", 24, 12,
                PlateType: "diplomatic", PlateNoOverride: "CMD 10-001"),
            new("Jeep", "Renegade 4xe", 2022, "M1", "Veleposlanistvo Zdruzenih drzav Amerike v Ljubljani", "LJ", "GENERALI", "AO", 12, 8,
                PlateType: "diplomatic", PlateNoOverride: "CD 10-237"),
            new("Ford", "Transit Custom", 2021, "N1", "Veleposlanistvo Zdruzenih drzav Amerike v Ljubljani", "LJ", "TRIGLAV", "AO", 10, 6,
                PlateType: "diplomatic", PlateNoOverride: "M 10-052"),
            new("Dacia", "Duster", 2023, "M1", "Avto Celje d.o.o.", "CE", "TRIGLAV", "AO", 3, 1,
                PlateType: "test", PlateNoOverride: "PR 07-521"),
            new("Peugeot", "508", 2022, "M1", "Intereuropa, d.d.", "LJ", "GENERALI", "AO", 5, 2,
                PlateType: "export", PlateNoOverride: "EX 18-402"),
            new("New Holland", "T5.100", 2021, "T1", "Panvita kmetijstvo d.o.o.", "MS", "TRIGLAV", "AO", 12, 6,
                PlateType: "agricultural", PlateNoOverride: "MS AG-105"),
            new("Boeckmann", "Cargo Trailer", 2020, "O2", "TPV d.o.o.", "KR", "TRIGLAV", "AO", 14, 8,
                PlateType: "trailer", PlateNoOverride: "452 AB-KR"),
            new("Tomos", "Flexer 50", 2018, "L1e", "Nejc Vidic", "LJ", "TRIGLAV", "AO", 9, 4,
                PlateType: "moped", PlateNoOverride: "LJ 3F-27")
        };

        seeds.AddRange(BuildGeneratedStandardVehicleSeeds());
        return seeds;
    }

    private static IEnumerable<VehicleSeedSpec> BuildGeneratedStandardVehicleSeeds()
    {
        var owners = new[]
        {
            "Klara Rozman", "Marko Hribar", "Nejc Vidic", "Lana Tomazic", "Ales Jerman", "Nika Smodis",
            "Urban Ceh", "Manca Lavric", "Maja Novak", "Andrej Kovac", "Nina Zupan", "Borut Vidmar",
            "Petra Mlakar", "Luka Kranjc", "Tjasa Horvat", "Miha Potocnik", "Eva Bizjak", "Jure Kos"
        };

        var companies = new[]
        {
            "Intereuropa, d.d.", "Porsche Inter Auto, d.o.o.", "Posta Slovenije d.o.o.",
            "DARS d.d.", "Luka Koper, d.d.", "Petrol d.d., Ljubljana",
            "Elektro Ljubljana d.d.", "Kolektor CPG d.o.o.", "Nomago d.o.o.",
            "Telekom Slovenije, d.d.", "Slovenske zeleznice, d.o.o."
        };

        var prefixes = new[] { "LJ", "MB", "CE", "KR", "KP", "NM", "GO", "MS", "PO", "SG", "PT", "KK" };
        var catalog = new (string Make, string Model, int Year, string CategoryCode)[]
        {
            ("Renault", "Clio", 2020, "M1"),
            ("Renault", "Captur", 2021, "M1"),
            ("Renault", "Megane", 2019, "M1"),
            ("Renault", "Austral", 2023, "M1"),
            ("Nissan", "Qashqai", 2020, "M1"),
            ("Nissan", "Juke", 2021, "M1"),
            ("Nissan", "Micra", 2018, "M1"),
            ("Peugeot", "2008", 2022, "M1"),
            ("Peugeot", "308 SW", 2021, "M1"),
            ("Citroen", "C5 X", 2023, "M1"),
            ("Citroen", "C3 Aircross", 2020, "M1"),
            ("Volkswagen", "Golf Variant", 2021, "M1"),
            ("Volkswagen", "Passat Variant", 2020, "M1"),
            ("Volkswagen", "T-Roc", 2022, "M1"),
            ("Audi", "Q3", 2021, "M1"),
            ("Audi", "A4 Avant", 2020, "M1"),
            ("BMW", "118d", 2021, "M1"),
            ("BMW", "X3", 2022, "M1"),
            ("Mercedes-Benz", "C 200", 2021, "M1"),
            ("Mercedes-Benz", "Sprinter", 2020, "N1"),
            ("Opel", "Insignia", 2019, "M1"),
            ("Opel", "Combo", 2021, "N1"),
            ("Fiat", "Panda", 2018, "M1"),
            ("Fiat", "Doblo", 2020, "N1"),
            ("Alfa Romeo", "Giulia", 2021, "M1"),
            ("Ford", "Kuga", 2022, "M1"),
            ("Ford", "Transit Custom", 2021, "N1"),
            ("Skoda", "Octavia", 2021, "M1"),
            ("Skoda", "Superb", 2020, "M1"),
            ("Skoda", "Kodiaq", 2022, "M1"),
            ("Seat", "Leon", 2021, "M1"),
            ("Seat", "Ateca", 2020, "M1"),
            ("Dacia", "Duster", 2021, "M1"),
            ("Dacia", "Sandero", 2022, "M1"),
            ("Kia", "Ceed", 2021, "M1"),
            ("Kia", "Sportage", 2023, "M1"),
            ("Hyundai", "i30", 2021, "M1"),
            ("Hyundai", "Tucson", 2022, "M1"),
            ("Volvo", "V60", 2020, "M1"),
            ("Volvo", "XC40", 2022, "M1"),
            ("Tesla", "Model Y", 2023, "M1"),
            ("Peugeot", "Expert", 2021, "N1"),
            ("Renault", "Master", 2020, "N1"),
            ("Volkswagen", "Crafter", 2021, "N1"),
            ("Mercedes-Benz", "Vito", 2022, "N1"),
            ("Citroen", "Jumpy", 2020, "N1"),
            ("Nissan", "Townstar", 2022, "N1"),
            ("Ford", "Focus", 2021, "M1"),
            ("Audi", "A6", 2022, "M1"),
            ("BMW", "520d", 2021, "M1"),
            ("Skoda", "Fabia", 2023, "M1"),
            ("Opel", "Corsa", 2023, "M1"),
            ("Fiat", "Tipo Cross", 2022, "M1"),
            ("Toyota", "Corolla Touring Sports", 2022, "M1"),
            ("Toyota", "Yaris Cross", 2023, "M1"),
            ("Toyota", "RAV4 Hybrid", 2022, "M1"),
            ("Toyota", "Proace City", 2021, "N1"),
            ("Kia", "Niro", 2022, "M1"),
            ("Hyundai", "Kona", 2023, "M1"),
            ("Volvo", "XC60", 2021, "M1"),
            ("Renault", "Arkana", 2022, "M1"),
            ("Peugeot", "408", 2023, "M1"),
            ("Nissan", "X-Trail", 2023, "M1"),
            ("Volkswagen", "Caddy", 2022, "N1")
        };

        for (var i = 0; i < catalog.Length; i++)
        {
            var currentParty = i % 4 == 0
                ? companies[i % companies.Length]
                : owners[i % owners.Length];

            var previousParty = i % 3 == 0
                ? owners[(i + 5) % owners.Length]
                : i % 5 == 0
                    ? companies[(i + 2) % companies.Length]
                    : null;

            var currentOwnershipMonthsAgo = 4 + (i % 24);
            var currentRegistrationMonthsAgo = 2 + (i % 12);
            int? previousOwnershipMonthsAgo = previousParty is null ? null : currentOwnershipMonthsAgo + 18 + (i % 12);
            int? previousRegistrationMonthsAgo = previousParty is null ? null : currentRegistrationMonthsAgo + 12 + (i % 12);
            var insurerCode = (i % 4) switch
            {
                0 => "TRIGLAV",
                1 => "GENERALI",
                2 => "SAVA",
                _ => "GRAWE"
            };

            var templateCode = catalog[i].CategoryCode != "M1"
                ? "AO"
                : insurerCode switch
                {
                    "GRAWE" => i % 2 == 0 ? "DELNI_KASKO" : "AO",
                    _ => i % 3 == 0 ? "KASKO" : "AO"
                };

            yield return new VehicleSeedSpec(
                catalog[i].Make,
                catalog[i].Model,
                catalog[i].Year,
                catalog[i].CategoryCode,
                currentParty,
                prefixes[i % prefixes.Length],
                insurerCode,
                templateCode,
                currentOwnershipMonthsAgo,
                currentRegistrationMonthsAgo,
                previousParty,
                previousOwnershipMonthsAgo,
                previousRegistrationMonthsAgo);
        }
    }

    private static string BuildRegistrationNo(int year, int sequence)
        => $"SI-{year % 100:D2}-{sequence:D6}";

    private static string BuildPolicyNo(string insurerCode, int sequence)
        => $"{insurerCode}-{sequence:D6}";

    private static decimal CalculatePremium(string categoryCode, int year, bool isCurrent)
    {
        var basePremium = categoryCode is "N1" or "N2" or "N3" ? 540m : 320m;
        var ageFactor = Math.Max(0, DateTime.Today.Year - year) * 7m;
        return isCurrent ? basePremium + ageFactor : basePremium + ageFactor - 25m;
    }

    private static int CalculateOdometer(int year, int sequence, bool isCurrent)
    {
        var ageYears = Math.Max(1, DateTime.Today.Year - year);
        var baseKm = ageYears * 18250;
        return baseKm + (sequence * 1330) + (isCurrent ? 850 : 0);
    }

    private sealed record VehicleSeedSpec(
        string Make,
        string Model,
        int Year,
        string CategoryCode,
        string CurrentPartyName,
        string PlatePrefix,
        string InsurerCode,
        string TemplateCode,
        int CurrentOwnershipMonthsAgo,
        int CurrentRegistrationMonthsAgo,
        string? PreviousPartyName = null,
        int? PreviousOwnershipMonthsAgo = null,
        int? PreviousRegistrationMonthsAgo = null,
        string PlateType = "standard",
        string? PlateNoOverride = null,
        string? RegistrationNoOverride = null);
}
