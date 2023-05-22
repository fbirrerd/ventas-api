using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace api_ventas.Models.Tables
{

    [PrimaryKey(nameof(consorcio_id), nameof(empresa_id), nameof(unegocio_id), nameof(perfil_id), nameof(usuario))]
    public class TAcceso
    {

        [Key]
        public string token { set; get; }
        public DateTime fecha_creacion { set; get; }
        public DateTime fecha_termino { set; get; }
        [Required]
        public string usuario { set; get; }
        [Required]
        public Int64 consorcio_id { set; get; }
        [Required]
        public Int64 empresa_id { set; get; }
        [Required]
        public Int64 unegocio_id { set; get; }
        [Required]
        public Int64 perfil_id { set; get; }
    }
}
