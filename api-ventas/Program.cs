using api_ventas.Models.Data;
using api_ventas.Models.Routes;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cnnString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<VentasDB>(options => options.UseNpgsql(cnnString));

builder.Services.AddControllers()
                .AddFluentValidation(opciones =>
                {
                    // Validate child properties and root collection elements
                    opciones.ImplicitlyValidateChildProperties = true;
                    opciones.ImplicitlyValidateRootCollectionElements = true;

                    // Automatic registration of validators in assembly
                    opciones.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error");
}
var grupo = app.MapGroup("/api/v1");
//RoutesMantenedores.ActiveRoutes(grupo);
RoutesProducto.ActiveRoutes(grupo);
RoutesStock.ActiveRoutes(grupo);
RoutesVenta.ActiveRoutes(grupo);

app.Run();