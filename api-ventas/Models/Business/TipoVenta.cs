using api_ventas.Models.Data;
using api_ventas.Models.Tables;

namespace api_ventas.Models.Business
{
    public class TipoVenta
    {

        public static bool existeTipoVenta(string sigla, VentasDB Db)
        {
            bool exist = false;
            int contar = (from e in Db.TipoVenta
                          where e.tipo_venta_sigla.Equals(sigla)
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }
        public static List<TTipoVenta> traeTipoVenta(VentasDB Db)
        {
            List<TTipoVenta> lista = (from e in Db.TipoVenta
                                      select e).ToList();

            return lista;
        }

    }
}
