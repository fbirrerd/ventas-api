using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api_ventas.Models.Tables
{
    [Table("venta")]
    public class TVenta
    {
        [Key]
        [Required]
        public long venta_id { get; set; }
        public DateTime fecha_venta { get; set; }
        public long empresa_id { get; set; }
        public long unegocio_id { get; set; }
        public string usuario_creador { get; set; }
        public decimal documento { get; set; }
        public string tipo_venta_sigla { get; set; }
        [NotMapped]
        public List<TVentaDetalle> detalle { get; set; }
        public decimal neto { get; set; }
        public decimal impuesto { get; set; }
        public decimal total { get; set; }
    }
}
