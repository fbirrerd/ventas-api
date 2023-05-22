using api_ventas.Models.Business;
using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NuGet.Protocol;

namespace api_ventas.Models.Routes
{
    public class RoutesVenta
    {
        public static void ActiveRoutes(RouteGroupBuilder app)
        {
            ActiveRoutesVenta(app);
        }

        public static void ActiveRoutesVenta(RouteGroupBuilder app)
        {
            app.MapPost("/venta", async (iDoc doc, VentasDB Db) =>
            {
                try
                {
                    RespuestaVenta r = new RespuestaVenta();
                    r = Venta.realizarVenta(doc, Db);
                    return Results.Ok(r);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                    
                }
                
            });

        }



    }
}
