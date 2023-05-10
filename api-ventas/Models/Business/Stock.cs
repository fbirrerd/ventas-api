using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using api_ventas.Models.Tables;

namespace api_ventas.Models.Business
{
    public class Stock
    {
        public static bool GenerarMovimientoBodega(iMovimientoStock oMov, VentasDB db)
        {
            try {
                if (oMov == null)
                {
                    throw new Exception("Datos enviados incomppletos");
                }
                TStock? t = null;
                t = db.Stock.Where(
                    e => e.producto_id == oMov.producto_id &&
                    e.empresa_id == oMov.empresa_id &&
                    e.unegocio_id == oMov.unegocio_id).FirstOrDefault();

                if (t == null)
                {
                    t = new TStock
                    {
                        stock_id = 0,
                        empresa_id = oMov.empresa_id,
                        producto_id = oMov.producto_id,
                        unegocio_id = oMov.unegocio_id,
                        cant_disponible = oMov.cantidad,
                        cant_reserva = 0,
                        cant_merma = 0,
                        cant_historico = oMov.cantidad
                        
                    };
                    db.Stock.Add(t);
                }
                else 
                {
                    if (oMov.tipo_movimiento.Equals("S"))
                    {
                        if (oMov.cantidad > t.cant_disponible) {
                            throw new Exception("No existe stock disponible");
                        }
                        t.cant_disponible -= oMov.cantidad;
                    }
                    if (oMov.tipo_movimiento.Equals("I")) {
                        t.cant_disponible += oMov.cantidad;
                        t.cant_historico +=  oMov.cantidad;
                    }
                }
                TStockMovimiento sm = new TStockMovimiento();
                sm.cantidad = oMov.cantidad;
                sm.producto_id = oMov.producto_id;
                sm.usuario = oMov.usuario;
                sm.unegocio_id = oMov.unegocio_id;
                sm.creation_date = DateTime.Now.ToUniversalTime();
                sm.empresa_id = oMov.empresa_id;
                sm.tipo_movimiento = oMov.tipo_movimiento;

                db.StockMovimiento.Add(sm);
                var r = db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
                            
            }
        }

    }
}
