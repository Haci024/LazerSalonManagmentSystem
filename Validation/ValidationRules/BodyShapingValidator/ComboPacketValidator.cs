using DTO.DTOS.BodyShapingDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.BodyShapingValidator
{
    public class ComboPacketValidator : AbstractValidator<BodyShapingComboDTO>
    {
        public ComboPacketValidator()
        {        
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət 0 və ya boş ola bilməz!");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Paket seçimi edin!");
        }
    }
}
