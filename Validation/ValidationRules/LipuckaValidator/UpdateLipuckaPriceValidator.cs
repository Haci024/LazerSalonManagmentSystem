using DTO.DTOS.LipuckaDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.LipuckaValidator
{
    public class UpdateLipuckaPriceValidator:AbstractValidator<LipuckaPriceUpdateDTO>
    {
        public UpdateLipuckaPriceValidator()
        {
            RuleFor(x=>x.Description).NotEmpty().WithMessage("Açıqlama olmadan qiymət dəyişə bilməz!");

           
        }
    }
}
