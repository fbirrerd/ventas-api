using api_ventas.Models.Business;
using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using api_ventas.Models.Tables;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace api_ventas.Models.Routes
{
    public class RoutesStock
    {
        public static void ActiveRoutes(RouteGroupBuilder app)
        {
            ActiveRoutesStock(app);
        }

        public static void ActiveRoutesStock(RouteGroupBuilder app)
        {
            app.MapPost("/stock", async (
                    iMovimientoStock obj,
                    IValidator<iMovimientoStock> validator,
                    VentasDB db) =>
            {
                var validationResult = validator.Validate(obj);
                if (validationResult.IsValid)
                {
                    try
                    {
                        var bandeera = Business.Stock.GenerarMovimientoBodega(obj, db);
                        return Results.Ok(bandeera);
                    }
                    catch (Exception ex)
                    {
                        return Results.Problem(ex.Message);
                    }
                }
                else
                {
                    return Results.ValidationProblem(validationResult.ToDictionary(),
                        statusCode: (int)HttpStatusCode.UnprocessableEntity);
                }
            });
            app.MapPost("/stock/{empresa}/{producto}", async (
                long empresa,
                long producto,
                VentasDB db) =>
            {
                //buscar datos de producto
                //id, nombre, unidadMedida, Medida, stockDisponible
                try {
                    oProducto oProd = Stock.getDatosProducto(empresa, producto);
                    Results.Ok(oProd);
                }
                catch (Exception ex) {
                    Results.Problem(ex.Message);                
                }


            });
        }
    }
}
