using api_ventas.Models.Data;
using api_ventas.Models.Objects;
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
                int r = 10000;





                return Results.Ok(r);
            });

        }



    }
}
