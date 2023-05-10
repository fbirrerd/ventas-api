using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using NuGet.Protocol;

namespace api_ventas.Models.Routes
{
    public class RoutesVenta
    {
        public static void ActiveRoutes(WebApplication app)
        {
            ActiveRoutesVenta(app);
        }

        public static void ActiveRoutesVenta(WebApplication app)
        {
            app.MapPost("/venta", async (iDoc doc, VentasDB db) =>
            {
                int r = 10000;

                //revisarDetalle




                return Results.Ok(r);
            });

        }



    }
}
