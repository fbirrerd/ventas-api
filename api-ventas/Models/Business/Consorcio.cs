using api_ventas.Models.Data;


namespace api_ventas.Models.Business
{
    public class Consorcio
    {

        private VentasDB Db;
        public Consorcio(VentasDB Db)
        {
            this.Db = Db;
        }
        public bool existCorsorcioXNombre(string nombre)
        {
            bool exist = false;
            int contar = (from e in Db.Consorcio
                          where e.nombre == nombre
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }
        public bool existCorsorcioXId(long id)
        {
            bool exist = false;
            int contar = (from e in Db.Consorcio
                          where e.consorcio_id == id
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }



    }
}
