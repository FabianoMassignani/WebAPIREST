using System.Text.Json.Serialization;
using WebAPIREST.Interfaces;
using WebAPIREST.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPessoaRepository, PessoaRepository >();
builder.Services.AddTransient<ITelefoneRepository, TelefoneRepository>();
builder.Services.AddControllers().AddJsonOptions(x=>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
