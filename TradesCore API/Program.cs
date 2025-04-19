using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Repository_Layer.Repositories;
using TradesCore_API.Utilities;
using Data_Layer.Models;
using Data_Layer.Mappings;
using Scalar.AspNetCore;
using Data_Layer.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IAdminActions, AdminActions>();
builder.Services.AddScoped<IPublicRepo, PublicRepo>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers().AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TradesCoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LukhanyoDatabase"))); //Change to proper DB Connection String on your side.

builder.Services.AddIdentity<TradesCoreUser, IdentityRole>().AddEntityFrameworkStores<TradesCoreDbContext>().AddDefaultTokenProviders();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{ 
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AppSettings:Audience"],
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
        ValidateIssuerSigningKey = true,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.AddDefaultAdminAccs();

app.Run();
