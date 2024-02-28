using DTO.DTOS.SolariumDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.SolariumValidator
{
    public class InjectionPacketValidator:AbstractValidator<InjectionPacketDTO>
    {
        public InjectionPacketValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama olmadan imtina edilə və ya pul qaytarıla bilməz!");
        }
    }
}
