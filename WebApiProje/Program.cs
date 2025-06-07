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
        // JSON ��kt�s�n� PascalCase tutmak i�in (ResultObject, IsSuccess vs.)
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Veritaban� ba�lant�s� (Dapper i�in)
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

// HTTPS y�nlendirme
app.UseHttpsRedirection();

// Kimlik Do�rulama ve Yetkilendirme
app.UseAuthentication();
app.UseAuthorization();

// CORS aktif et
app.UseCors("AllowOrigin");

// Statik dosyalar (opsiyonel)
app.UseStaticFiles();

// IoC servis sa�lay�c�
ServiceTool.ServiceProvider = app.Services;

// Controller routing
app.MapControllers();

app.Run();
