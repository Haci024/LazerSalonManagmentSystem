using DTO.DTOS.OutMoneyDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.OutMoneyValidator
{
    public class OutMoneyValidator:AbstractValidator<OutMoneyAddDTO>
    {
        public OutMoneyValidator()
        {
       
            RuleFor(x => x.Product).NotEmpty().WithMessage("Başlıq boş ola bilməz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama boş ola bilməz");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət 0 ola bilməz");
        }

    }
}
