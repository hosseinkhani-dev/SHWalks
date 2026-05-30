using AutoMapper;

using Microsoft.EntityFrameworkCore;
using SHWalks.API.Mappings;
using SHWalks.Application.Areas;
using SHWalks.Application.Walks;
using SHWalks.Infrastructure.Persistence;
using SHWalks.Infrastructure.Repositories.Areas;
using SHWalks.Infrastructure.Repositories.Walks;

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
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
