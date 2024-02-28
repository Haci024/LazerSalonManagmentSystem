using DTO.DTOS.CosmetologyDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.CosmetologyValidator
{
    public class CosmetologyCategoryValidator:AbstractValidator<AddCosmetologyCategoryDTO>
    {
        public CosmetologyCategoryValidator()
        {
            RuleFor(x => x.MainCategoryId).NotEmpty().WithMessage("Əsas kategoriyanı seçin ...");
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kateqoriya adı boş ola bilməz ...");
        }
    }
}
