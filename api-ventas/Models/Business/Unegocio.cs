using api_ventas.Models.Data;

namespace api_ventas.Models.Business
{
    public class Unegocio
    {

        public static bool existUnegocioXNombre(string nombre, long empresa_id, VentasDB Db)
        {
            bool exist = false;
            int contar = (from e in Db.UNegocio
                          where e.nombre == nombre
                          && e.empresa_id == empresa_id
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }
        public static bool existUNegocioXId(long id, VentasDB Db)
        {
            bool exist = false;
            int contar = (from e in Db.UNegocio
                          where e.unegocio_id == id
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }
        public static bool existUNegocioEmpresa(long empresa_id, long uNegocio_id, VentasDB Db)
        {
            bool exist = false;
            int contar = (from e in Db.UNegocio
                          where e.empresa_id == empresa_id
                          &&   e.unegocio_id == uNegocio_id
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }
    }
}
