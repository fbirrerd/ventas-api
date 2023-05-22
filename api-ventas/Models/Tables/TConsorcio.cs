using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_ventas.Models.Tables
{
    [Table("consorcio")]
    public class TConsorcio
    {

        [Key]
        [Required]
        public long consorcio_id { set; get; }
        [Required]
        [StringLength(100)]
        public string nombre { set; get; }
        [Required]
        public DateTime fecha_creacion { set; get; }
        [Required]
        public int estado { set; get; }
    }
}
