using DTO.DTOS.LazerAppointmentDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.LazerValidator
{
    public class LazerCategoryValidator:AbstractValidator<AddLazerCategoryDTO>
    {
        public LazerCategoryValidator()
        {
            RuleFor(x=>x.FilialId).NotEmpty().WithMessage("Filial seçimi  etmədiniz!");
            RuleFor(x => x.MainCategoryId).NotEmpty().WithMessage("Əsas kateqoriya seçmədiniz!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət 0 ola bilməz!");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nahiyə adı boş ola bilməz!");
        }
    }
}
