using EventLogsAPI.Data;
using EventLogsAPI.Repositories;
using EventLogsAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configurar la cadena de conexi�n a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar los repositorios y servicios para la inyecci�n de dependencias
builder.Services.AddScoped<IEventLogRepository, EventLogRepository>();
builder.Services.AddScoped<EventLogService>();

// Configurar Swagger para la documentaci�n de la API
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
