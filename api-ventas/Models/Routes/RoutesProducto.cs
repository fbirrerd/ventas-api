using api_ventas.Models.Business;
using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using api_ventas.Models.Tables;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace api_ventas.Models.Routes
{
    public class RoutesProducto
    {
        public static void ActiveRoutes(RouteGroupBuilder app)
        {
            ActiveRoutesStock(app);
        }

        public static void ActiveRoutesStock(RouteGroupBuilder app)
        {
            app.MapGet("/catproducto", (VentasDB Db) =>
            {
                return Db.CatProducto.ToList()
                is List<TCatProducto> l
                    ? Results.Ok(l)
                    : Results.NotFound();
            });
            app.MapGet("/catproducto/{id}", (int id, VentasDB Db) =>
            {
                return Db.CatProducto.FirstOrDefault(e => e.categoria_producto_id.Equals(id))
                is TCatProducto l
                    ? Results.Ok(l)
                    : Results.NotFound();
            });
            app.MapPost("/catproducto", async (
                    TCatProducto obj,
                    IValidator<TCatProducto> validator,
                    VentasDB Db) =>
                {
                    var validationResult = validator.Validate(obj);
                    if (validationResult.IsValid)
                    {
                        _ = Db.CatProducto.Add(obj);
                        await Db.SaveChangesAsync();
                        return Results.Ok();
                    }
                    else
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary(),
                            statusCode: (int)HttpStatusCode.UnprocessableEntity);
                    }
                });
            app.MapPut("/catproducto", async (
                TCatProducto obj,
                IValidator<TCatProducto> validator,
                VentasDB Db) =>
            {

                var objNew = await Db.CatProducto.FindAsync(obj.categoria_producto_id);
                if (objNew == null)
                {
                    return Results.NotFound();
                }
                objNew.nombre = obj.nombre;
                objNew.empresa_id = obj.empresa_id;
                await Db.SaveChangesAsync();
                return Results.Ok();
            });
            app.MapDelete("/catproducto", async (int id, VentasDB Db) =>
            {
                var registro = await Db.CatProducto.FindAsync(id);
                if (registro == null)
                {
                    return Results.NotFound();
                }

                Db.CatProducto.Remove(registro);
                await Db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}
