using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Repository_Layer.Repositories;
using Service_Layer.EmailService;
using TradesCore_API.Utilities;
using Service_Layer.IEmail;
using Data_Layer.Mappings;
using Data_Layer.Models;
using Scalar.AspNetCore;
using Data_Layer.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IPublicRepo, PublicRepo>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
builder.Services.AddScoped<IEmailService, EmailServices>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddControllers().AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();

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
