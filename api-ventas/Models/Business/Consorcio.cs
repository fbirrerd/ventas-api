using api_ventas.Models.Data;
using api_ventas.Models.Tables;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace api_ventas.Models.Business
{
    public class Consorcio
    {

        private VentasDB db;
        public Consorcio(VentasDB db) {
            this.db = db;
        }
        public bool existCorsorcioXNombre(string nombre)
        {
            bool exist = false;
            int contar = (from e in db.Consorcio
                          where e.nombre == nombre
                          select e).Count();

            if (contar > 0) exist= true;
            return exist;
        }
        public bool existCorsorcioXId(long id)
        {
            bool exist = false;
            int contar = (from e in db.Consorcio
                          where e.consorcio_id == id
                          select e).Count();

            if (contar > 0) exist = true;
            return exist;
        }



    }
}
