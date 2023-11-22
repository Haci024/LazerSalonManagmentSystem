using DTO.DTOS.InComeDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.IncomeValidator
{
    public class IncomeValidator:AbstractValidator<InComeAddDTO>
    {
        public IncomeValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət boş ola bilməz");
            RuleFor(x => x.StockId).NotEmpty().WithMessage("Məhsul seçməyi unutdunuz!");
            RuleFor(x => x.Count).NotEmpty().WithMessage("Say 0 ola bilməz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama boş ola bilməz!");
        }
    }
}
