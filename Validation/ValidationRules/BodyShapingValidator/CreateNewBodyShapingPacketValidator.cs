using DTO.DTOS.BodyShapingDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.BodyShapingValidator
{
    public class CreateNewBodyShapingPacketValidator:AbstractValidator<AddBodyShapingPacketDTO>
    {
        public CreateNewBodyShapingPacketValidator()
        {
            RuleFor(x => x.Packet).NotEmpty().WithMessage("Paket adı boş ola bilməz!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Paketin qiyməti 0 ola bilməz!");
            RuleFor(x => x.SessionCount).NotEmpty().WithMessage("Seans sayı 0 ola bilməz!");
            RuleFor(x => x.MainCategoryId).NotEmpty().WithMessage("Hansı Seans tipinə aid olduğunu seçin!");
            RuleFor(x => x.SessionDuration).NotEmpty().WithMessage(" Seans müddətini qeyd edin!");
        }
    }
}
