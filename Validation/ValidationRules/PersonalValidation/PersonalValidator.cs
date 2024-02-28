using DTO.DTOS.PersonalDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.PersonalValidation
{
    public class PersonalValidator:AbstractValidator<NewPersonalDTO>
    {
        public PersonalValidator()
        {
            RuleFor(x => x.FilialId).NotEmpty().WithMessage("Filial seçin!");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Ad daxil edin!");
        }
    }
}
