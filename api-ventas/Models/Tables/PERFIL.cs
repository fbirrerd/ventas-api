using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_ventas.Models.Tables
{
    [Table("perfil")]
    public class PERFIL
    {

        [Required]
        [Key]
        public long perfil_id { set; get; }
        [StringLength(50)]
        [Required]
        public string nombre { set; get; }
        public long? empresa_id { set; get; }
        public int estado { set; get; }
    }
}
