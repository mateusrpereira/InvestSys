using Application.Active;
using Application.Active.Ports;
using Application.Portfolio;
using Application.Portfolio.Ports;
using Application.Transaction;
using Application.Transaction.Ports;
using Application.User;
using Application.User.Ports;
using Data;
using Data.Active;
using Data.Portfolio;
using Data.Transaction;
using Data.User;
using Domain.Active.Ports;
using Domain.Portfolio.Ports;
using Domain.Ports;
using Domain.Transaction.Ports;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

# region IoC
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IActiveManager, ActiveManager>();
builder.Services.AddScoped<IActiveRepository, ActiveRepository>();

builder.Services.AddScoped<IPortfolioManager, PortfolioManager>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

builder.Services.AddScoped<ITransactionManager, TransactionManager>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
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
