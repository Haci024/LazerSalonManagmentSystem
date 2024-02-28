using DTO.DTOS.BodyShapingDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.BodyShapingValidator
{
    public class NewSessionValidator:AbstractValidator<NewSessionDTO>
    {
        public NewSessionValidator()
        {
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Giriş saatını qeyd edin!");
            RuleFor(x=>x.StartDate).NotEmpty().WithMessage("Çıxış saatını qeyd edin!");
            RuleFor(x => x.Duration).NotEmpty().WithMessage("Seans müddəti 0 ola bilməz!");
           
        }
    }
}
