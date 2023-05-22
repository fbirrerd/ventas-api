using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api_ventas.Models.Tables
{
    [Table("tipo_venta")]
    public class TTipoVenta
    {
        [Key]
        [StringLength(5)]
        public string tipo_venta_sigla { set; get; }
        [Required]
        [StringLength(50)]
        public string nombre { set; get; }


    }
}
