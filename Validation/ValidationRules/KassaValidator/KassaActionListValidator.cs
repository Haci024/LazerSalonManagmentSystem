﻿using Business.IServices;
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
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama olmadan əməliyyat yerinə yetirilə bilməz!");
            RuleFor(x => x.ProcessDate).NotEmpty().WithMessage("Tarixi qeyd etməyi unutdunuz!");
            


        }
     
    }
}
