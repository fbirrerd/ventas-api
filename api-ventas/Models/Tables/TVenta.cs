using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
namespace api_ventas.Models.Tables
{
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



    }
}
