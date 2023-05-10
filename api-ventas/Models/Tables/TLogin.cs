using System.ComponentModel.DataAnnotations;

namespace api_ventas.Models.Tables
{
    public class TLogin
    {
        [StringLength(20)]
        [Required]
        [Key]
        public string usuario { set; get; }
        [StringLength(20)]
        [Required]
        public string clave { set; get; }
        public DateTime fecha_creacion { set; get; }
        [StringLength(50)]
        [Required]
        [EmailAddress]
        public string email { set; get; }
    }
}
