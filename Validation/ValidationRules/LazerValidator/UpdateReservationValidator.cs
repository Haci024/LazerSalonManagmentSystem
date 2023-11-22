using DTO.DTOS.LazerAppointmentDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.LazerValidator
{
    public class UpdateReservationValidator:AbstractValidator<UpdateReservationDTO>
    {
        public UpdateReservationValidator()
        {
            RuleFor(x=>x.StartTime).NotEmpty().WithMessage("Giriş saatını daxil edin...");
            RuleFor(x => x.LazerMasterName).NotEmpty().WithMessage("Lazer ustasının adını qeyd edin!");
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Müştəri adı boş ola bilməz!");
           
         RuleFor(x => x.StartTime).Must(BeValidTime).WithMessage("Saat 00:00 ola bilməz.");

        }
        private bool BeValidTime(DateTime? endTime)
        {
            if (endTime.HasValue)
            {
                // Eğer saat 00:00 ise bu geçersizdir.
                return endTime.Value.TimeOfDay != TimeSpan.Zero;
            }

            return true;
        }

    }
}
