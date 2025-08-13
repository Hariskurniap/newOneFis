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
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// ─────────────────────────────────────
// 🔧 CONFIGURATION
// ─────────────────────────────────────
builder.Services.Configure<SapSynchSettings>(builder.Configuration.GetSection("SapSynch"));
builder.Services.AddHttpClient(); // Untuk IHttpClientFactory
builder.Services.AddHttpClient<ISapSynchService, SapSynchService>();

// ─────────────────────────────────────
// 🧩 DEPENDENCY INJECTION
// ─────────────────────────────────────
builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();
builder.Services.AddScoped<IMongoRepository, MongoRepository>();
builder.Services.AddScoped<IListDOService, ListDOService>();

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

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Paste JWT token only (with 'Bearer' prefix).",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
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
    //c.DocInclusionPredicate((docName, apiDesc) =>
    //{
    //    if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;

    //    var groupName = apiDesc.GroupName ??
    //        apiDesc.ActionDescriptor?.RouteValues["controller"];

    //    return groupName == docName;
    //});
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

// Aktifkan authentication & authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Hapus dulu middleware JWT custom agar tidak konflik
// app.UseMiddleware<WebAPI.Middleware.JwtMiddleware>();

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
