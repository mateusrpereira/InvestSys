using Application.User;
using Application.User.Ports;
using Data;
using Data.User;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

# region IoC
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region DB wiring up
var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<PortfolioDbContext>(
    options => options.UseSqlServer(connectionString));
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
