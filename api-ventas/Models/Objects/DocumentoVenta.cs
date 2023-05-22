namespace api_ventas.Models.Objects
{
    public class DocumentoVenta
    {
        public string documento { get; set; }
        public List<VentaDetalle> detalle { get; set; }
        public VentaTotal total { get; set; }
        public List<string> errores { get; set; }

    }

    public class VentaDetalle
    {
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
            this.total = monto * cantidad;
        }
    }



    public class VentaTotal
    {
        public decimal neto { get; set; }
        public decimal total { get; set; }
        public decimal impuesto { get; set; }

        public VentaTotal(decimal total, decimal impuesto, decimal neto)
        {
            this.total = total;
            this.impuesto = impuesto;
            this.neto = neto;
        }
        public VentaTotal(decimal total, decimal porcentajeImpuestoVenta)
        {
            this.total = decimal.Round(total, 0);
            this.neto = (total / (100 + porcentajeImpuestoVenta) * 100);
            this.neto = decimal.Round(this.neto, 0);
            this.impuesto = total - this.neto;
        }
    }
}
