using api_ventas.Models.Business;
using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using api_ventas.Models.Tables;
using FluentValidation;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
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
                    VentasDB Db) =>
            {
                var validationResult = validator.Validate(obj);
                if (validationResult.IsValid)
                {
                    try
                    {
                        var bandera = Business.Stock.GenerarMovimientoBodega(obj, Db);
                        return Results.Ok(bandera);
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
            app.MapPost("/stock/producto", async (
                iProducto producto,
                VentasDB Db) =>
            {
                //buscar datos de producto
                //id, nombre, unidadMedida, Medida, stockDisponible
                try {
                    oProducto oProd = Stock.getDatosProducto(
                       producto, 
                        Db);
                    return Results.Ok(oProd);
                }
                catch (Exception ex) {
                    return Results.NotFound(ex.Message);                
                }


            });
        }
    }
}
