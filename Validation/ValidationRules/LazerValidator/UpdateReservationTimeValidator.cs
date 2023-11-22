using DTO.DTOS.LazerAppointmentDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.LazerValidator
{
    public class UpdateReservationTimeValidator:AbstractValidator<UpdateReservationTimeDTO>
    {
        public UpdateReservationTimeValidator()
        {
            RuleFor(x => x.NewStartTime).NotEmpty().WithMessage("Rezervasiya saatı boş ola bilməz");
        }
    }
}
