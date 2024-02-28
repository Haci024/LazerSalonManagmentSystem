using DTO.DTOS.SolariumDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.SolariumValidator
{
    public class SolariumValidator : AbstractValidator<SolariumAddDTO>
    {


        public SolariumValidator()
        {

            RuleFor(x => x.Price).NotEmpty().WithMessage("Paketin qiyməti 0 ola bilməz!");
            RuleFor(x => x.MinuteLimit).NotEmpty().WithMessage("Paket istifadə müddəti 0 ola bilməz!");
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Solarium paketini alacaq müştərinin adını və soyadını daxil edin!");
            RuleFor(x => x.ChildCategoryId).NotEmpty().WithMessage("Paket seçimi etmədiniz!");
       
        }


    }


}

