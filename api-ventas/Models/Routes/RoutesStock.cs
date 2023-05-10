using api_ventas.Models.Business;
using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using api_ventas.Models.Tables;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace api_ventas.Models.Routes
{
    public class RoutesStock
    {
        public static void ActiveRoutes(RouteGroupBuilder app)
        {
            ActiveRoutesCatProducto(app);
            ActiveRoutesTipoMedida(app);
            ActiveRoutesProducto(app);
        }

        public static void ActiveRoutesCatProducto(RouteGroupBuilder app)
        {
            app.MapGet("/catproducto", (VentasDB db) =>
            {
                return db.CatProducto.ToList()
                is List<TCatProducto> l
                    ? Results.Ok(l)
                    : Results.NotFound();
            });
            app.MapGet("/catproducto/{id}", (int id, VentasDB db) =>
            {
                return db.CatProducto.FirstOrDefault(e => e.categoria_producto_id.Equals(id))
                is TCatProducto l
                    ? Results.Ok(l)
                    : Results.NotFound();
            });
            app.MapPost("/catproducto", async (
                    TCatProducto obj,
                    IValidator<TCatProducto> validator,
                    VentasDB db) =>
                {
                    var validationResult = validator.Validate(obj);
                    if (validationResult.IsValid)
                    {
                        _ = db.CatProducto.Add(obj);
                        await db.SaveChangesAsync();
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
                VentasDB db) =>
            {

                var objNew = await db.CatProducto.FindAsync(obj.categoria_producto_id);
                if (objNew == null)
                {
                    return Results.NotFound();
                }
                objNew.nombre = obj.nombre;
                objNew.empresa_id = obj.empresa_id;
                await db.SaveChangesAsync();
                return Results.Ok();
            });
            app.MapDelete("/catproducto", async (int id, VentasDB db) =>
            {
                var registro = await db.CatProducto.FindAsync(id);
                if (registro == null)
                {
                    return Results.NotFound();
                }

                db.CatProducto.Remove(registro);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
        public static void ActiveRoutesTipoMedida(RouteGroupBuilder app)
        {
            app.MapGet("/tipomedida", (VentasDB db) =>
            {
                return db.TipoMedida.ToList()
                is List<TTipoMedida> l
                    ? Results.Ok(l)
                    : Results.NotFound();
            });
            app.MapGet("/tipomedida/{id}", (int id, VentasDB db) =>
            {
                return db.TipoMedida.FirstOrDefault(e => e.tipo_medida_id.Equals(id))
                is TTipoMedida l
                    ? Results.Ok(l)
                    : Results.NotFound();
            });
            app.MapPost("/tipomedida", async (
                    TTipoMedida obj,
                    IValidator<TTipoMedida> validator,
                    VentasDB db) =>
            {
                var validationResult = validator.Validate(obj);
                if (validationResult.IsValid)
                {
                    _ = db.TipoMedida.Add(obj);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }
                else
                {
                    return Results.ValidationProblem(validationResult.ToDictionary(),
                        statusCode: (int)HttpStatusCode.UnprocessableEntity);
                }
            });
            app.MapPut("/tipomedida", async (
                TTipoMedida obj,
                IValidator<TTipoMedida> validator,
                VentasDB db) =>
            {

                var objNew = await db.TipoMedida.FindAsync(obj.tipo_medida_id);
                if (objNew == null)
                {
                    return Results.NotFound();
                }
                objNew.nombre = obj.nombre; 
                objNew.empresa_id = obj.empresa_id;
                await db.SaveChangesAsync();
                return Results.Ok();
            });
            app.MapDelete("/tipomedida", async (int id, VentasDB db) =>
            {
                var registro = await db.CatProducto.FindAsync(id);
                if (registro == null)
                {
                    return Results.NotFound();
                }

                db.CatProducto.Remove(registro);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
        public static void ActiveRoutesProducto(RouteGroupBuilder app)
        {
            app.MapGet("/producto", (VentasDB db) =>
            {
                return db.Producto.ToList()
                is List<TProducto> l
                    ? Results.Ok(l)
                    : Results.NotFound();
            });
            app.MapGet("/producto/{id}", (int id, VentasDB db) =>
            {
                return db.Producto.FirstOrDefault(e => e.tipo_medida_id.Equals(id))
                is TProducto l
                    ? Results.Ok(l)
                    : Results.NotFound();
            });
            app.MapPost("/producto", async (
                    TProducto obj,
                    IValidator<TProducto> validator,
                    VentasDB db) =>
            {
                var validationResult = validator.Validate(obj);
                if (validationResult.IsValid)
                {
                    _ = db.Producto.Add(obj);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }
                else
                {
                    return Results.ValidationProblem(validationResult.ToDictionary(),
                        statusCode: (int)HttpStatusCode.UnprocessableEntity);
                }
            });
            app.MapPut("/producto", async (
                TProducto obj,
                IValidator<TProducto> validator,
                VentasDB db) =>
            {

                var objNew = await db.Producto.FindAsync(obj.tipo_medida_id);
                if (objNew == null)
                {
                    return Results.NotFound();
                }
                objNew.nombre1 = obj.nombre1;
                objNew.empresa_id = obj.empresa_id;
                await db.SaveChangesAsync();
                return Results.Ok();
            });
            app.MapDelete("/producto", async (int id, VentasDB db) =>
            {
                var registro = await db.Producto.FindAsync(id);
                if (registro == null)
                {
                    return Results.NotFound();
                }

                db.Producto.Remove(registro);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}
