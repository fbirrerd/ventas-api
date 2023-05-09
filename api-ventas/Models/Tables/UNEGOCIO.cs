using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace api_ventas.Models.Tables
{
    [Table("unegocio")]
    public class UNEGOCIO
    {
        [Key]
        public long unegocio_id { set; get; }
        [Required]
        [StringLength(100)]
        public string nombre { set; get; }
        public DateTime fecha_creacion { set; get; }
        [Required]
        public int estado { set; get; }
        public long empresa_id { set; get; }
    }
}
