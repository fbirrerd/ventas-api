using api_ventas.Models.Business;
using api_ventas.Models.Tables;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.KeyPerFile;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using api_ventas.Models.Objects;
using api_ventas.Models.Data;
using api_ventas.Models.Routes;
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cnnString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<VentasDB>(options => options.UseNpgsql(cnnString));

builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    // Validate child properties and root collection elements
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;

                    // Automatic registration of validators in assembly
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
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


app.Run();