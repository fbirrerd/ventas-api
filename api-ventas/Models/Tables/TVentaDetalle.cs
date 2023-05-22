using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
namespace api_ventas.Models.Tables
{
    public class TVentaDetalle
    {
        [Key]
        [Required]
        public long vdetalle_id { get; set; }
        public long venta_id { get; set; }
        public long producto_id { get; set; }
        public decimal cantidad { get; set; }
        public decimal valor { get; set; }
        public decimal impuesto { get; set; }
        public decimal total { get; set; }
    }
}
