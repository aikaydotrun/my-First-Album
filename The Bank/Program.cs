using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using TheBank.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TheBank.Models;
using TheBank.AuthService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CustomerDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerDb")));
builder.Services.AddDbContext<StaffDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("StaffDb")));



builder.Services.AddScoped<TokenProvider>();
builder.Services.AddScoped<loginService>();

builder.Services.AddAuthorization();


var secretKey = builder.Configuration["JwtSettings:secretKey"];
if (string.IsNullOrEmpty(secretKey))
{
    Console.WriteLine("SECRET KEY: " + secretKey);
    throw new Exception("SecretKey cannot be null.");
}
var Key = Encoding.UTF8.GetBytes(secretKey);



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.RequireHttpsMetadata = false;
       options.SaveToken = true;
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
           ValidAudience = builder.Configuration["JwtSettings:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Key)
       };
   });

builder.Services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheBank API", Version = "v1" });
var jwtSecurityScheme = new OpenApiSecurityScheme
{
    Scheme = "bearer",
    BearerFormat = "JWT",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Description = "Put only your JWT Bearer token here",
    Reference = new OpenApiReference
    {
        Type = ReferenceType.SecurityScheme,
        Id = JwtBearerDefaults.AuthenticationScheme
    }
 };
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheBank API v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

