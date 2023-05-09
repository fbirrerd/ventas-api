using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace api_ventas.Models.Tables
{
    [Table("empresa")]
    public class EMPRESA
    {

        [Key]
        public Int64 empresa_id { set; get; }
        [Required]
        [StringLength(100)]
        public string nombre { set; get; }
        public DateTime fecha_creacion{ set; get; }
        [Required]
        public int estado { set; get; }
        [Required]
        public Int64 consorcio_id { set; get; }
    }
}
