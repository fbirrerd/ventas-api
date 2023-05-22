using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_ventas.Models.Tables
{
    [Table("stock")]
    public class TStock
    {
        [Key]
        [Required]
        public long stock_id { set; get; }
        [Required]
        public long empresa_id { set; get; }
        [Required]
        public long unegocio_id { set; get; }
        [Required]
        public long producto_id { set; get; }
        [Required]
        public decimal cant_historico { set; get; }
        public decimal cant_disponible { set; get; }
        public decimal cant_merma { set; get; }
        public decimal cant_reserva { set; get; }
    }
}
