using DTO.DTOS.BodyShapingDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.BodyShapingValidator
{
    public class InjectionPacketValidator:AbstractValidator<InjectionPacketDTO>
    {
        public InjectionPacketValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama olmadan imtina edilə və ya pul qaytarıla bilməz!");
        }
    }
}
