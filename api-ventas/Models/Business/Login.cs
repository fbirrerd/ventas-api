using api_ventas.Models.Data;

namespace api_ventas.Models.Business
{
    public class Login
    {
        private VentasDB db;
        public Login(VentasDB db)
        {
            this.db = db;
        }
        public bool existLoginXLogin(string nombre)
        {
            bool exist = false;
            int contar = (from e in db.Perfil
                          where e.nombre == nombre
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }
        public bool existLoginXCorreo(string email)
        {
            bool exist = false;
            int contar = (from e in db.Login
                          where e.email == email
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }

    }
}