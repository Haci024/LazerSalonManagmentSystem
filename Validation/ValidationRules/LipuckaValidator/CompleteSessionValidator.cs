using DTO.DTOS.LipuckaDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.LipuckaValidator
{
    public class CompleteSessionValidator:AbstractValidator<CompleteLipuckaAppointmentDTO>
    {
        public CompleteSessionValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət 0 ola bilməz!");
            RuleFor(x => x.EndTime).NotEmpty().WithMessage("Çıxış saatı daxil edilməyib!");
           
        }
    }
}
