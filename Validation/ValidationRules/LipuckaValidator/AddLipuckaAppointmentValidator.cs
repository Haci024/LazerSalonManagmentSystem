using DTO.DTOS.LazerAppointmentDTO;
using DTO.DTOS.LipuckaDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.LipuckaValidator
{
    public class AddLipuckaAppointmentValidator : AbstractValidator<AddNewAppointmentDTO>
    {
        public AddLipuckaAppointmentValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət 0 ola bilməz!");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Çıxış saatı daxil edilməyib!");
            RuleFor(x => x.LipuckaCategoryIds).NotEmpty().WithMessage("Nahiyə seçimi etmədiniz!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama qeyd edin...");
        }

    }
}
