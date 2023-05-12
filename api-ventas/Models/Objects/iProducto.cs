using NetTopologySuite.GeometriesGraph;

namespace api_ventas.Models.Objects
{
    public class iProducto
    {
        public long empresa_id { get; set; }
        public long unegocio_id { get; set; }
        public string codigoProducto { get; set; }
    }
}
