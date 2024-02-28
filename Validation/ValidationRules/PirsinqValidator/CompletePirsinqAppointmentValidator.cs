using DTO.DTOS.PirsinqDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.PirsinqValidator
{
    public class CompletePirsinqAppointmentValidator:AbstractValidator<CompletePirsinqAppointmentDTO>
    {
        public CompletePirsinqAppointmentValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət 0 ola bilməz!");
            RuleFor(x => x.EndTime).NotEmpty().WithMessage("Çıxış saatı daxil edilməyib!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama boş ola bilməz!");
        }
    }
}
