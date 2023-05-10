using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace api_ventas.Models.Tables
{
    [Table("stock_movimiento")]
    [PrimaryKey(nameof(empresa_id), nameof(unegocio_id), nameof(producto_id), nameof(usuario), nameof(creation_date), nameof(tipo_movimiento))]
    public class TStockMovimiento
    {
        [Required]
        public long empresa_id { set; get; }
        [Required]
        public long unegocio_id { set; get; }
        [Required]
        public long producto_id { set; get; }
        public string usuario { set; get; }
        [Required]
        public DateTime creation_date { set; get; }
        public string tipo_movimiento { set; get; }
        public decimal cantidad { set; get; }
        
    }
}
