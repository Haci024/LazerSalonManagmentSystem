using DTO.DTOS.BodyShapingDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.BodyShapingValidator
{
    public class UpdatePriceValidator:AbstractValidator<UpdatePriceDTO>
    {
        public UpdatePriceValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama olmadan qiymət dəyişə bilməz!");
        }
    }
}
