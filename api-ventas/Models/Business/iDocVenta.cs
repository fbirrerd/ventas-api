using api_ventas.Models.Data;

namespace api_ventas.Models.Business
{
    public class IDocVenta
    {
        private VentasDB db;
        public IDocVenta(VentasDB db)
        {
            this.db = db;
        }
        public bool existUNegocioXId(long id)
        {
            bool exist = false;
            int contar = (from e in db.UNegocio
                          where e.unegocio_id == id
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }

    }
}
