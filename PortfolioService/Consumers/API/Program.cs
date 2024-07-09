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
<<<<<<< HEAD
using Domain.Transaction.Ports;
using Microsoft.EntityFrameworkCore;
=======
using Domain.Security;
using Domain.Transaction.Ports;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
>>>>>>> 6c3766f (Implementação da autenticação - JWT Token)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

<<<<<<< HEAD
=======
var signingConfigurations = new SigningConfigurations();
builder.Services.AddSingleton(signingConfigurations);

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
    .Configure(tokenConfigurations);

builder.Services.AddSingleton(tokenConfigurations);

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    var paramsValidation = bearerOptions.TokenValidationParameters;
    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
    paramsValidation.ValidAudience = tokenConfigurations.Audience;
    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
    paramsValidation.ValidateIssuerSigningKey = true;
    paramsValidation.ValidateLifetime = true;
    paramsValidation.ClockSkew = TimeSpan.Zero;
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                   .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                   .RequireAuthenticatedUser().Build());
});

>>>>>>> 6c3766f (Implementação da autenticação - JWT Token)
builder.Services.AddControllers();

# region IoC
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ILoginManager, LoginManager>();

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
<<<<<<< HEAD
builder.Services.AddSwaggerGen();
=======
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Entre com o Token JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            }, new List<string>()
        }
    });
});
>>>>>>> 6c3766f (Implementação da autenticação - JWT Token)

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
