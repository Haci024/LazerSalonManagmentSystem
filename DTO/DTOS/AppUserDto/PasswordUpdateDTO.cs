using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.AppUserDto
{
    public class PasswordUpdateDTO
    {
        public string Password { get; set; }    

        public string ConfirmPassword { get; set; }

        public string UserName {  get; set; }  
    }
}
