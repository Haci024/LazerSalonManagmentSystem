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
       
            RuleFor(x => x.SpendCategoryId).NotEmpty().WithMessage("Kategoriya seçimi edin!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama boş ola bilməz");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət 0 ola bilməz");
        }

    }
}
