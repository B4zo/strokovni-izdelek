using Microsoft.OpenApi.Models;
using API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
// DB Connection string
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("App")));
// Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        var key = builder.Configuration["Jwt:Key"]!;
        var issuer = builder.Configuration["Jwt:Issuer"]!;
        var audience = builder.Configuration["Jwt:Audience"]!;

        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply migrations and the runtime grants with the elevated connection before the app user starts querying tables.
using (var scope = app.Services.CreateScope())
{
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var migrationConnectionString = configuration.GetConnectionString("Migrations");

    if (!string.IsNullOrWhiteSpace(migrationConnectionString))
    {
        var migrationOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(migrationConnectionString)
            .Options;

        await using var migrationDb = new AppDbContext(migrationOptions);
        await migrationDb.Database.MigrateAsync();

        await migrationDb.Database.ExecuteSqlRawAsync("""
            CREATE EXTENSION IF NOT EXISTS pg_trgm;

            GRANT USAGE ON SCHEMA tehnicni TO app;
            GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA tehnicni TO app;
            GRANT USAGE, SELECT, UPDATE ON ALL SEQUENCES IN SCHEMA tehnicni TO app;
            ALTER DEFAULT PRIVILEGES FOR ROLE db_admin IN SCHEMA tehnicni
            GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO app;
            ALTER DEFAULT PRIVILEGES FOR ROLE db_admin IN SCHEMA tehnicni
            GRANT USAGE, SELECT, UPDATE ON SEQUENCES TO app;

            CREATE INDEX IF NOT EXISTS ix_parties_address_trgm
            ON tehnicni.parties USING gin (address gin_trgm_ops);

            CREATE INDEX IF NOT EXISTS ix_parties_phone_trgm
            ON tehnicni.parties USING gin (phone gin_trgm_ops);

            CREATE INDEX IF NOT EXISTS ix_parties_email_trgm
            ON tehnicni.parties USING gin (email gin_trgm_ops);

            CREATE INDEX IF NOT EXISTS ix_people_full_name_trgm
            ON tehnicni.people USING gin (full_name gin_trgm_ops);

            CREATE INDEX IF NOT EXISTS ix_people_tax_no_trgm
            ON tehnicni.people USING gin (tax_no gin_trgm_ops);

            CREATE INDEX IF NOT EXISTS ix_people_emso_trgm
            ON tehnicni.people USING gin (emso gin_trgm_ops);

            CREATE INDEX IF NOT EXISTS ix_companies_company_name_trgm
            ON tehnicni.companies USING gin (company_name gin_trgm_ops);

            CREATE INDEX IF NOT EXISTS ix_companies_tax_no_trgm
            ON tehnicni.companies USING gin (tax_no gin_trgm_ops);

            CREATE INDEX IF NOT EXISTS ix_companies_company_reg_no_trgm
            ON tehnicni.companies USING gin (company_reg_no gin_trgm_ops);

            CREATE INDEX IF NOT EXISTS ix_plates_plate_no_trgm
            ON tehnicni.plates USING gin (plate_no gin_trgm_ops);
            """);
    }

    var appDb = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await AppDbSeed.SeedAsync(appDb);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

