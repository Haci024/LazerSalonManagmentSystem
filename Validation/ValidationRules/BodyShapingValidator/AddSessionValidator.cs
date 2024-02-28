using DTO.DTOS.BodyShapingDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.BodyShapingValidator
{
    public class AddSessionValidator:AbstractValidator<AddSessionDTO>
    {
        public AddSessionValidator()
        {
            RuleFor(x=>x.SessionCount).NotEmpty().WithMessage("Seans sayını qeyd etmədiniz!");
            RuleFor(x => x.Duration).NotEmpty().WithMessage("Seans sayı müddətini qeyd etmədiniz!");
        }
    }
}
