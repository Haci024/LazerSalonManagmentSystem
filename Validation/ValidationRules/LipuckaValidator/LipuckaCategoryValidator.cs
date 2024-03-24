using DTO.DTOS.LipuckaDTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.LipuckaValidator
{
    public class LipuckaCategoryValidator:AbstractValidator<AddLipuckaCategoryDTO>
    {
        public LipuckaCategoryValidator()
        {
            RuleFor(x => x.MainCategoryId).NotEmpty().WithMessage("Əsas kateqoriya boş ola bilməz!");
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kateqoriya adı boş ola bilməz!");
        }
    }
}
