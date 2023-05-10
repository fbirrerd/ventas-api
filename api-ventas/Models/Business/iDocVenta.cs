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


    }
}
