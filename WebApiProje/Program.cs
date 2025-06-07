using System.Data;
using System.Data.SqlClient;
using Core.DataAccess.Dapper;
using WebApiProje.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Swagger ve Controller
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // JSON çýktýsýný PascalCase tutmak için (ResultObject, IsSuccess vs.)
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Veritabaný baðlantýsý (Dapper için)
builder.Services.AddScoped<IDbConnection>(_ =>
    new SqlConnection(builder.Configuration.GetConnectionString("DbLocalConnection")));
builder.Services.AddScoped<IDapperRepository, DapperRepository>();

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

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
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

// CORS
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", policy =>
        policy.WithOrigins(
                "http://domain.com",
                "http://domain2.com",
                "http://localhost:3000",
                "https://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// HTTPS yönlendirme
app.UseHttpsRedirection();

// Kimlik Doðrulama ve Yetkilendirme
app.UseAuthentication();
app.UseAuthorization();

// CORS aktif et
app.UseCors("AllowOrigin");

// Statik dosyalar (opsiyonel)
app.UseStaticFiles();

// IoC servis saðlayýcý
ServiceTool.ServiceProvider = app.Services;

// Controller routing
app.MapControllers();

app.Run();
