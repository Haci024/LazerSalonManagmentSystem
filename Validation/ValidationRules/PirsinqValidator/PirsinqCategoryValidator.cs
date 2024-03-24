using DTO.DTOS.PirsinqDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.PirsinqValidator
{
    public class PirsinqCategoryValidator:AbstractValidator<AddPirsinqCategoryDTO>
    {
        public PirsinqCategoryValidator()
        {
         
            RuleFor(x => x.MainCategoryId).NotEmpty().WithMessage("Əsas kateqoriya boş ola bilməz");
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Ad boş ola bilməz!");
        }
    }
}
