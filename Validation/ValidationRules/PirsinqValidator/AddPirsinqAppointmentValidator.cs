using DTO.DTOS.PirsinqDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.PirsinqValidator
{
    public class AddPirsinqAppointmentValidator:AbstractValidator<AddPirsinqAppointmentDTO>
    {
        public AddPirsinqAppointmentValidator()
        {
            RuleFor(x => x.PirsinqCategoryIds).NotEmpty().WithMessage("Kategoriya seçimi etmədiniz!");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Giriş saatını daxil edin!");
           
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət 0 ola bilməz!");

        }
    }
}
