using api_ventas.Models.Data;

namespace api_ventas.Models.Business
{
    public class Perfil
    {
        private VentasDB Db;
        public Perfil(VentasDB Db)
        {
            this.Db = Db;
        }
        public bool existPerfilXNombre(string nombre)
        {
            bool exist = false;
            int contar = (from e in Db.Perfil
                          where e.nombre == nombre
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }
        public bool existPerfilXId(long id)
        {
            bool exist = false;
            int contar = (from e in Db.Perfil
                          where e.perfil_id == id
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }

    }
}
