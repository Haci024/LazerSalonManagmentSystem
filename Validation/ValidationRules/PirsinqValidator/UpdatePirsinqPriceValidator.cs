using DTO.DTOS.PirsinqDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.PirsinqValidator
{
    public class UpdatePirsinqPriceValidator:AbstractValidator<PirsinqPriceUpdateDTO>
    {
        public UpdatePirsinqPriceValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama olmadan qiymət dəyişə bilməz!");
        }
    }
}
