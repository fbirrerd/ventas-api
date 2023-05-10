using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace api_ventas.Models.Tables
{
    [Table("tipo_medida")]
    public class TTipoMedida
    {
        [Key]
        public long tipo_medida_id { set; get; }
        public string nombre { set; get; }
        public string signo { set; get; }
        public int estado { set; get; }
        public Nullable<long> empresa_id { set; get; }
        public class Validator : AbstractValidator<TTipoMedida>
        {
            public Validator()
            {
                RuleFor(x => x.signo).NotNull().Length(1, 20).NotEmpty().WithMessage("Debe enviar un signo para el tipo de medida");
                RuleFor(x => x.nombre).NotNull().Length(1, 100).NotEmpty().WithMessage("Debe enviar un mombre para el tipo de medida");
                RuleFor(x => x.estado).NotNull().InclusiveBetween(0,1).WithMessage("Debe enviar un estado");
            }
        }

    }
}
