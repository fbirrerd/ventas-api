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
            _ = app.MapPost("/venta", async (iDoc doc, VentasDB Db) =>
            {
                try
                {
                    //VALIDACIONES
                    //******************************************************
                    //se valida que exista toda la venta en el inventario

                    List<string> errores = new List<string>();

                    foreach (var d in doc.Details)
                    {
                        //if (!Stock.existStockDisponible(doc.empresa_id, d.producto_id, d.cantidad))
                        //{
                        //    errores.Add(String.Format("No hay stock disponible para producto {0}", d.producto_id));
                        //}
                        //else
                        //{
                        //    //generar reserva
                        //}
                    }
                }
                catch (Exception ex)
                {
                    
                }





            });

        }



    }
}
