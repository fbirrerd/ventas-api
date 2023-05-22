using api_ventas.Models.Data;

namespace api_ventas.Models.Business
{
    public class Perfil
    {
        public bool existPerfilXNombre(string nombre, VentasDB Db)
        {
            bool exist = false;
            int contar = (from e in Db.Perfil
                          where e.nombre == nombre
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }
        public bool existPerfilXId(long id, VentasDB Db)
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
