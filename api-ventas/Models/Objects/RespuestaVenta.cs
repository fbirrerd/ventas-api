using System.Security.Policy;

namespace api_ventas.Models.Objects
{
    public class RespuestaVenta
    {
        public string documento { get; set; }
        public List<VentaDetalle> detalle { get; set; }
        public VentaTotal total { get; set; }
        public List<string> errores { get; set; }

    }

    public class VentaDetalle{
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad { get; set; }
        public decimal monto { get; set; }
        public decimal total { get; set; }
    
        public VentaDetalle(string codigo, string descripcion, decimal cantidad, decimal monto)
        {
            this.codigo = codigo;
            this.descripcion = descripcion;
            this.cantidad = cantidad;
            this.monto = monto;
            this.total = monto*cantidad;
        }
    }



    public class VentaTotal
    {
        public decimal neto { get; set; }
        public decimal total { get; set; }
        public decimal impuesto { get; set; }

        public VentaTotal(decimal total, int porcentajeImpuesto)
        {
            this.total = decimal.Round(total,0) ;
            this.neto = (total / (100 + porcentajeImpuesto) * 100);
            this.impuesto = total - this.neto;
        }
    }
}
