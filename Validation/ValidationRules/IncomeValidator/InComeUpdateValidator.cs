using DTO.DTOS.InComeDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.IncomeValidator
{
    public class InComeUpdateValidator:AbstractValidator<UpdateInComeDTO>
    {
        public InComeUpdateValidator()
        {
           
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama  boş ola bilmz!");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Gəlir adı boş ola bilmz!");
            RuleFor(x => x.Count).NotEmpty().WithMessage("Say 0 ola bilməz!");




        }
    }
}
