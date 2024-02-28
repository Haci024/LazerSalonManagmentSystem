using DTO.DTOS.SolariumDTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.SolariumValidator
{
    public class SolariumPacketValidator:AbstractValidator<NewSolariumPacketDTO>
    {
        public SolariumPacketValidator()
        {
            RuleFor(x=>x.MainCategoryId).NotEmpty().WithMessage("Paketin tipini seçmədiniz!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Paketin qiymətini daxil etməyi unutdunuz!");
            RuleFor(x => x.UsingPeriod).NotEmpty().WithMessage("Paketin aktiv qalma müddətini qeyd edin!");
            RuleFor(x => x.Minute).NotEmpty().WithMessage("Paketin limitini qeyd edin!");
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Paketin adını qeyd etmədiniz!");


        }
    }
}
