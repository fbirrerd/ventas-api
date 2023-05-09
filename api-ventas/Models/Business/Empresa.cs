using api_ventas.Models.Data;

namespace api_ventas.Models.Business
{
    public class Empresa
    {
        private VentasDB db;
        public Empresa(VentasDB db)
        {
            this.db = db;
        }
        public bool existEmpresaXNombre(string nombre)
        {
            bool exist = false;
            int contar = (from e in db.Empresa
                          where e.nombre == nombre
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }
        public bool existEmpresaXId(long id, long consorcio_id)
        {
            bool exist = false;
            int contar = (from e in db.Empresa
                          where e.empresa_id == id
                          && e.consorcio_id == consorcio_id
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }

    }
}
