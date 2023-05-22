using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
