using apbd_lab_12.Models;
using apbd_lab_12.Repositories;
using apbd_lab_12.Repositories.Interfaces;
using apbd_lab_12.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<TripService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


app.UseHttpsRedirection();

app.MapControllers();

app.Run();