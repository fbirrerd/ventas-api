using NetTopologySuite.GeometriesGraph;

namespace api_ventas.Models.Objects
{
    public class oProducto
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal cantidad { get; set; }
        public string medida { get; set; }

        public oProducto(int id, string codigo, string nombre, decimal cantidad, string medida) { 
            this.id = id;
            this.codigo = codigo;
            this.nombre = nombre;
            this.cantidad = cantidad;
            this.medida = medida;
        }

    }
}
