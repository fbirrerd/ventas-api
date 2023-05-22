using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api_ventas.Models.Tables
{
    [Table("folio")]
    public class TFolio
    {
        [Key]
        public long folio_id { set; get; }
        public long empresa_id { set; get; }
        public long? unegocio_id { set; get; }
        public string tipo_venta_sigla { set; get; }
        public long folio { set; get; }

    }
}
