﻿using api_ventas.Models.Tables;
using FluentValidation;

namespace api_ventas.Models.Objects
{
    public class iMovimientoStock
    {
        public long stock_id { set; get; }
        public long empresa_id { set; get; }
        public long unegocio_id { set; get; }
        public string usuario { set; get; }
        public long producto_id { set; get; }
        public string tipo_movimiento { set; get; }
        public decimal cantidad { set; get; }
        public class Validator : AbstractValidator<iMovimientoStock>
        {
            public Validator()
            {
                //RuleFor(x => x.categoria_producto_id).NotEmpty().WithMessage("Debe enviar un id");
                //RuleFor(x => x.empresa_id).NotNull().NotEmpty().WithMessage("Debe enviar el id de la empresa");
                List<string> ListtipoMovimiento = new List<string>() {"E", "S"};
                RuleFor(x => x.tipo_movimiento)
                    .NotNull().WithMessage("Se debe enviar un valor para el tipo e movimiento")
                    .Must(tm => !ListtipoMovimiento.Contains(tm)).WithMessage("Los valores permitidos para el tipo de movimiento son: " + ListtipoMovimiento.ToString());

            }
        }

    }
}
