using System;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebAPIREST;
using WebAPIREST.Interfaces;
using WebAPIREST.Repository;
using WebAPIREST.Services;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder
    .Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        policy =>
        {
            policy.RequireRole("admin");
        }
    );
    options.AddPolicy("Employee",
        policy =>
        {
            policy.RequireRole("employee");
        }
    );
});

// Add services
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserRepository, PessoaRepository>();
builder.Services.AddTransient<ITelefoneRepository, TelefoneRepository>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();

builder
    .Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
