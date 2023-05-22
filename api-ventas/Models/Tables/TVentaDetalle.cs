using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api_ventas.Models.Tables
{
    [Table("venta_detalle")]
    public class TVentaDetalle
    {
        [Key]
        [Required]
        public long vdetalle_id { get; set; }
        public long venta_id { get; set; }
        public long producto_id { get; set; }
        public decimal cantidad { get; set; }
        public decimal valor { get; set; }
        public decimal total { get; set; }
    }
}
