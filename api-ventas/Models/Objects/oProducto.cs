using NetTopologySuite.GeometriesGraph;
using System.Security.Cryptography.X509Certificates;

namespace api_ventas.Models.Objects
{
    public class oProducto
    {
        public long producto_id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal cantidad { get; set; }
        public Medida medida { get; set; }

        public oProducto(long producto_id, string codigo, string nombre)
        {
            this.producto_id = producto_id;
            this.codigo = codigo;
            this.nombre = nombre;
        }
        public oProducto()
        {
        }

    }
    public class Medida
    {
        public decimal valor { get; set; }

        public string unidad { get; set; }
        public string descripcion{ get; set; }
    }

}
