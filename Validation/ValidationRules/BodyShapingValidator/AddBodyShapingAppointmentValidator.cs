using DTO.DTOS.BodyShapingDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.BodyShapingValidator
{
    public class AddBodyShapingAppointmentValidator:AbstractValidator<AddBodyShapingAppointmentDTO>
    {
        public AddBodyShapingAppointmentValidator()
        {
            RuleFor(x => x.ChildCategoryId).NotEmpty().WithMessage("Paket seçməyi unutdunuz!");         
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət 0 və ya boş ola bilməz!");
            
        }
    }
}
