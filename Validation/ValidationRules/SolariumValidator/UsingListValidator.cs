using DTO.DTOS.SolariumDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.SolariumValidator
{
    public class UsingListValidator : AbstractValidator<UsingListAddAppointmentDTO>
    {
        public UsingListValidator()
        {
            RuleFor(x => x.UsingMinute).NotEmpty().WithMessage("İstifadə olunan müddət 0 ola bilməz!");

        }
    }
}
