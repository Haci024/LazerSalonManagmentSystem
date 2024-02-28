using DTO.DTOS.LazerAppointmentDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.LazerValidator
{
    public class UpdatePriceValidator:AbstractValidator<LazerPriceUpdateDTO>
    {
        public UpdatePriceValidator()
        {
           
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama olmadan qiymət dəyişə bilməz");
        }
    }
}
