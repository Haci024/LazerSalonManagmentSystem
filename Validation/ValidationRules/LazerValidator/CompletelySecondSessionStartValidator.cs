using DTO.DTOS.LazerAppointmentDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.LazerValidator
{
    public class CompletelySecondSessionStartValidator:AbstractValidator<CompletelySecondSessionStartDTO>
    {
        public CompletelySecondSessionStartValidator()
        {
            RuleFor(x => x.StartDate2).NotEmpty().WithMessage("Seans tarixi boş ola bilməz");
                }
    }
}
