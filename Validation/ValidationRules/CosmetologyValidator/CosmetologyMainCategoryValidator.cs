using DTO.DTOS.CosmetologyDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.CosmetologyValidator
{
    public class CosmetologyMainCategoryValidator:AbstractValidator<AddCosmetologyMainCategoryDTO>
    {
        public CosmetologyMainCategoryValidator()
        {
            RuleFor(x=>x.CategoryName).NotEmpty().WithMessage("Kategoriya adı boş ola bilməz!");
        }
    }
}
