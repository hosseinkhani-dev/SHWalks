using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using SHWalks.API.Mappings;
using SHWalks.Application.Areas;
using SHWalks.Application.Walks;
using SHWalks.Infrastructure.Persistence;
using SHWalks.Infrastructure.Repositories.Areas;
using SHWalks.Infrastructure.Repositories.Walks;
using System.Text;
using SHWalks.Application.TokenServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inject DbContext
builder.Services.AddDbContext<SHWalksDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("SHWalksConnectionString")));

builder.Services.AddScoped<IAreaRepository, SqlAreaRepository>();
builder.Services.AddScoped<IAreaService, AreaAppService>();
builder.Services.AddScoped<IWalkRepository, SqlWalkRepository>();
builder.Services.AddScoped<IWalkService, WalkAppService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfiles));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
