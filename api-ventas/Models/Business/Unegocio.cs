using api_ventas.Models.Data;

namespace api_ventas.Models.Business
{
    public class Unegocio
    {
        private VentasDB Db;
        public Unegocio(VentasDB Db)
        {
            this.Db = Db;
        }
        public bool existUnegocioXNombre(string nombre, long empresa_id)
        {
            bool exist = false;
            int contar = (from e in Db.UNegocio
                          where e.nombre == nombre
                          && e.empresa_id == empresa_id
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }
        public bool existUNegocioXId(long id)
        {
            bool exist = false;
            int contar = (from e in Db.UNegocio
                          where e.unegocio_id == id
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }

    }
}
