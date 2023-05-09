using api_ventas.Models.Data;

namespace api_ventas.Models.Business
{
    public class Perfil
    {
        private VentasDB db;
        public Perfil(VentasDB db)
        {
            this.db = db;
        }
        public bool existPerfilXNombre(string nombre)
        {
            bool exist = false;
            int contar = (from e in db.Perfil
                          where e.nombre == nombre
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }
        public bool existPerfilXId(long id)
        {
            bool exist = false;
            int contar = (from e in db.Perfil
                          where e.perfil_id == id
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }

    }
}
