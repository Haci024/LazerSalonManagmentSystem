using DTO.DTOS.CustomerDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.CustomerValidator
{
    public class UpdateCustomerValidator:AbstractValidator<CustomerUpdateDTO>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Ad və Soyad boş ola bilməz!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz!");
            RuleFor(x => x.PhoneNumber).NotEmpty().Must(BeAValidPhoneNumber).WithMessage("Nömrəniz 9  rəqəmdən  ibarət olmalıdır.Öndəki 0 buna daxil deyil!");
            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Doğum tarixini qeyd etməyi unutdunuz!");
        }
        private bool BeAValidPhoneNumber(double phoneNumber)
        {

            return phoneNumber.ToString().Length == 9;
        }
    }

   
}

