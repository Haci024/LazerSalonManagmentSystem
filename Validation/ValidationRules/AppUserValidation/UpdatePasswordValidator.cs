using DTO.DTOS.AppUserDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.ValidationRules.AppUserValidation
{
    public class UpdatePasswordValidator:AbstractValidator<PasswordUpdateDTO>
    {
        public UpdatePasswordValidator()
        {
            RuleFor(x=>x.Password).NotEmpty().WithMessage("Şifrə boş ola bilməz!");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Şifrə minimum 6 simvoldan ibarət olmalıdır!");
            RuleFor(x=>x.ConfirmPassword).NotEmpty().WithMessage("Təkrar şifrə boş ola bilməz!").Equal(x=>x.Password).WithMessage("Şifrələr eyni deyil!");
            
        }
    }
}
