using DTO.DTOS.BodyShapingDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.BodyShapingValidator
{
    public class AddComboPacketCategoryValidator:AbstractValidator<NewComboPacketCategoryDTO>
    {
        public AddComboPacketCategoryValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiyməti daxil edin...");
            RuleFor(x => x.PacketName).NotEmpty().WithMessage("Paketin adını qeyd edin...");
            RuleFor(x => x.SessionCount).NotEmpty().WithMessage("Seans sayını qeyd edin...");
        }
    }
}
