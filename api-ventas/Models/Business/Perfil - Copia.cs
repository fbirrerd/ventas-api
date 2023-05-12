using api_ventas.Models.Data;

namespace api_ventas.Models.Business
{
    public class Login
    {
        private VentasDB Db;
        public Login(VentasDB Db)
        {
            this.Db = Db;
        }
        public bool existLoginXLogin(string nombre)
        {
            bool exist = false;
            int contar = (from e in Db.Perfil
                          where e.nombre == nombre
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }
        public bool existLoginXCorreo(string email)
        {
            bool exist = false;
            int contar = (from e in Db.Login
                          where e.email == email
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }

    }
}
