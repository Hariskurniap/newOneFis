using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;
using Domain.Entities;
using Application.Interfaces;
using Application.Services;
using Application.Settings;
using Infrastructure.Mongo;
using Infrastructure.Repositories;
using Microsoft.OData.Edm;

var builder = WebApplication.CreateBuilder(args);

// ─────────────────────────────────────
// 🔧 CONFIGURATION
// ─────────────────────────────────────
// MongoDB context singleton
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();

// Repository
builder.Services.AddScoped<IMongoRepository, MongoRepository>();

// Services
//Loading Order
builder.Services.AddScoped<IListDOService, ListDOService>();

//Shipment
builder.Services.AddScoped<IShipmentService, ShipmentService>();
builder.Services.AddScoped<ISapSynchService, SapSynchService>();
builder.Services.AddScoped<ISapShipmentService, SapShipmentService>();

//Checkin Onefis
builder.Services.AddScoped<ICheckinOnefisRepository, CheckinOnefisRepository>();
builder.Services.AddScoped<ICheckinOnefisService, CheckinOnefisService>();


// Checkin
builder.Services.AddScoped<ICheckinRepository, CheckinRepository>();
builder.Services.AddScoped<ICheckinService, CheckinService>();

//dcu
builder.Services.AddScoped<IDcuCheckinRepository, DcuCheckinRepository>();
builder.Services.AddScoped<IDcuCheckinService, DcuCheckinService>();

//DI
builder.Services.AddScoped<IDailyInspectionRepository, DailyInspectionRepository>();
builder.Services.AddScoped<IDailyInspectionService, DailyInspectionService>();

//pti
builder.Services.AddScoped<IPretripInspectionRepository, PretripInspectionRepository>();
builder.Services.AddScoped<IPretripInspectionService, PretripInspectionService>();

//shipmentChecklistActivity
builder.Services.AddScoped<IShipmentChecklistActivitiesRepository, ShipmentChecklistActivitiesRepository>();
builder.Services.AddScoped<IShipmentChecklistActivitiesService, ShipmentChecklistActivitiesService>();

//shipmentValidation
builder.Services.AddScoped<IShipmentValidationsRepository, ShipmentValidationsRepository>();
builder.Services.AddScoped<IShipmentValidationsService, ShipmentValidationsService>();

//PTI Transaction
builder.Services.AddScoped<ITransactionPretripInspectionsRepository, TransactionPretripInspectionsRepository>();
builder.Services.AddScoped<ITransactionPretripInspectionsService, TransactionPretripInspectionsService>();

//Master DI
builder.Services.AddScoped<IMasterDailyInspectionRepository, MasterDailyInspectionRepository>();
builder.Services.AddScoped<IMasterDailyInspectionService, MasterDailyInspectionService>();



// HttpClient
builder.Services.AddHttpClient();



// ─────────────────────────────────────
// 🧭 CONTROLLERS + ODATA
// ─────────────────────────────────────
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    })
    .AddOData(opt => opt.Select().Filter().OrderBy().SetMaxTop(100).Count()
        .AddRouteComponents("api", GetEdmModel()));

// ─────────────────────────────────────
// 🔐 JWT Authentication & Authorization
// ─────────────────────────────────────
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
{
    throw new Exception("JWT configuration is missing in appsettings.json");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Supaya bisa menerima token dengan atau tanpa "Bearer "
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(token) && !token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = "Bearer " + token;
                context.Request.Headers["Authorization"] = token;
            }
            return Task.CompletedTask;
        }
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();

// ─────────────────────────────────────
// 📒 SWAGGER
// ─────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "pertamina-onefis", Version = "v2" });

    // Setting supaya di Swagger cukup input token tanpa "Bearer "
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Masukkan JWT token saja (tanpa 'Bearer ' prefix)",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
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

// ─────────────────────────────────────
// ▶ APP PIPELINE
// ─────────────────────────────────────
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "pertamina-onefis v2");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// ─────────────────────────────────────
// 🧱 EDM Model for OData
// ─────────────────────────────────────
static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<ListDO>("listDO");
    return builder.GetEdmModel();
}
