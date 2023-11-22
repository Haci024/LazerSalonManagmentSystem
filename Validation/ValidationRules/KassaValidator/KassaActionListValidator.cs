using DTO.DTOS.KassaActionsDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.KassaValidator
{
    public class KassaActionListValidator:AbstractValidator<KassaActionsDTO>
    {
        public KassaActionListValidator()
        {
            RuleFor(x => x.OutMoney).NotEmpty().WithMessage("Çıxarılacaq pul 0 ola bilməz!");
            
        }
    }
}
