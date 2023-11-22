using DTO.DTOS.LazerAppointmentDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.LazerValidator
{
    public class LazerValidator : AbstractValidator<LazerAppointmentDTO>
    {
        public LazerValidator()
        {
            RuleFor(x => x.lazerCategoriesId).NotEmpty().WithMessage("Nahiyələri seçməyi unutdunuz!");
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Müştəri adını və soyadını daxil edin!");
            RuleFor(x => x.LazerMasterName).NotEmpty().WithMessage("Lazer ustasının adı boş ola bilməz");
            RuleFor(x => x.ReservationDate).NotEmpty().WithMessage("Rezervasiya tarixini və saatını qeyd edin!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət 0 ola bilməz");
        }
    }
}
