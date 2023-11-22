using DTO.DTOS.LazerAppointmentDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.LazerValidator
{
    public class CompletedReservationStatusValidator:AbstractValidator<CompletedReservationStatusDTO>
    {
        public CompletedReservationStatusValidator()
        {
            RuleFor(x => x.ImpulsCount).NotEmpty().WithMessage("İmpuls sayı boş ola bilməz");
            RuleFor(x => x.EndTime).NotEmpty().WithMessage("Çıxış 00:00  ola bilməz");
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Müştəri adı boş ola bilməz!");
            RuleFor(x => x.LazerMasterName).NotEmpty().WithMessage("Müştəri adı boş ola bilməz!");
        }
    }
}
