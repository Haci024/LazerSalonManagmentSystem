using DTO.DTOS.CosmetologyDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.CosmetologyValidator
{
    public class CompleteCosmetologyValidator:AbstractValidator<CompleteSessionDTO>
    {
        public CompleteCosmetologyValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Məbləğ 0 ola bilməz!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama boş ola bilməz!");
            RuleFor(x => x.OutDate).NotEmpty().WithMessage("Tarixi qeyd etmədiniz!");

        }
    }
}
