using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_ventas.Models.Tables
{
    [Table("categoria_producto")]
    public class TCatProducto
    {
        [Key]
        public long categoria_producto_id { set; get; }
        public long? empresa_id { set; get; }
        [Required]
        public string nombre { set; get; }

        public class Validator : AbstractValidator<TCatProducto>
        {
            public Validator()
            {
                //RuleFor(x => x.categoria_producto_id).NotEmpty().WithMessage("Debe enviar un id");
                //RuleFor(x => x.empresa_id).NotNull().NotEmpty().WithMessage("Debe enviar el id de la empresa");
                RuleFor(x => x.nombre).NotNull().Length(1, 50).NotEmpty().WithMessage("Debe enviar un mombre de la empresa válido");
            }
        }
    }
}
