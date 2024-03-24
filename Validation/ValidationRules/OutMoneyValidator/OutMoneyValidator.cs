using Business.IServices;
using DTO.DTOS.OutMoneyDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.OutMoneyValidator
{
    
    public class OutMoneyValidator:AbstractValidator<OutMoneyAddDTO>
    {
        private readonly IKassaService _kassaService;
        
        public OutMoneyValidator(IKassaService kassaService)

        {
            _kassaService = kassaService;
            RuleFor(x => x.SpendCategoryId).NotEmpty().WithMessage("Kategoriya seçimi edin!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıqlama boş ola bilməz!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymət 0 ola bilməz");
            RuleFor(x => x.Price).LessThanOrEqualTo(_kassaService.Budget(1)).WithMessage("Kassada kifayət qədər pul yoxdur!");

        }
        
      
        
    }
}
