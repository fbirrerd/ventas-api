﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace api_ventas.Models.Tables
{
    [Table("producto")]
    public class TProducto
    {
        [Key]
        public long producto_id { set; get; }
        public Nullable<long> empresa_id { set; get; }
    
        public long categoria_producto_id { set; get; }
  
        [StringLength(50)]
        public string codigo { set; get; }
        [Required]
        [StringLength(500)]
        public string nombre1 { set; get; }
        [StringLength(500)]
        public string nombre2 { set; get; }
        public long valor_venta { set; get; }
        [Required]
        public long tipo_medida_id { set; get; }
        public decimal valor_medida { set; get; }
        public class Validator : AbstractValidator<TProducto>
        {
            public Validator()
            {
                //RuleFor(x => x.categoria_producto_id).NotEmpty().WithMessage("Debe enviar un id");
                //RuleFor(x => x.empresa_id).NotNull().NotEmpty().WithMessage("Debe enviar el id de la empresa");
                RuleFor(x => x.nombre1).NotNull().Length(1, 50).NotEmpty().WithMessage("Debe enviar un mombre del producto");
            }
        }

    }
}
