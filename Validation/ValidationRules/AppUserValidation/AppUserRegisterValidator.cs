using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.DTOS.AppUserDto;

namespace Business.ValidationRules.AppUserValidation
{
    public class AppUserRegisterValidator:AbstractValidator<NewUserDTO>
    {
        public AppUserRegisterValidator()
        {

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifrə boş ola bilməz!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("İstifadəçi adı yalnızca hərf və rəqəmdən ibarət ola bilər!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("İstifadəçi adı boş ola bilməz!");            
			RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifrə təkrarı boş ola bilməz!");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Şifrələr eyni deyil!");
          
        }

    }
}
