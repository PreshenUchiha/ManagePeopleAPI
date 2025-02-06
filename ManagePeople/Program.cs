using ManagePeople;
using ManagePeople.Configuration;
using ManagePeople.Domains.Entities.Accounts;
using ManagePeople.Domains.Entities.Accounts.Repositories;
using ManagePeople.Domains.Entities.Persons;
using ManagePeople.Domains.Entities.Persons.Repositories;
using ManagePeople.Domains.Entities.Transactions.Repositories;
using Microsoft.OpenApi.Models;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("OpenAPISpecificationForPersons", new OpenApiInfo
    {
        Title = "Manage Db API [Persons]",
        Description = "This API provides functionality to manage people and their accounts",
        Version = "v1.0.0",
        Contact = new OpenApiContact
        {
            Name = "Preshen",
            Email = "preshenr@email.com"
        }
    });


    setup.AddSecurityDefinition("APIKeySecurityDefinition", new()
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "XApiKey",
        Description = "Please enter a valid API Key to access this API"
    });

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {{
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "APIKeySecurityDefinition"
            }
        },
        new List<string>()
    }});


});

var ManagePeopleDbConnectionString = builder.Configuration.GetConnectionString("ManagePeopleDb")
                                    ?? throw new InvalidOperationException("ConnectionStrings:ManagePeopleDb is missing in configuration");
builder.Services.AddPersonServices();
builder.Services.AddAccountServices();

builder.Services.AddScoped<Stopwatch>();

builder.Services.Configure<ConnectionStringsOptions>(
    builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.Configure<StoredProcedureOptions>(
    builder.Configuration.GetSection("StoredProcedures"));

builder.Services.Configure<SecurityOptions>(
    builder.Configuration.GetSection("Security"));

var app = builder.Build();

if (app.Configuration["EnableSwaggerUI"] is "true")
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint(
            url: "/swagger/OpenAPISpecificationForPersons/swagger.json",
            name: "Standby Connect API [Persons]");
    });

    app.Map("/", httpContext => Task.Run(() => httpContext.Response.Redirect("/swagger")))
       .ShortCircuit();
}

app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
