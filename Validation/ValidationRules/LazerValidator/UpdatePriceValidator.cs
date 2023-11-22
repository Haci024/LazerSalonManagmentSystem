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
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət 0 ola bilməz!");
            RuleFor(x => x.Desciption).NotEmpty().WithMessage("Açıqlama olmadan qiymət artırıla bilməz");
        }
    }
}
