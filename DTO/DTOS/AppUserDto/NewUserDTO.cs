using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.AppUserDto
{
    public class NewUserDTO
    {
       
        public string UserName { get; set; }

        public string Role {  get; set; }

        public List<IdentityRole> IdentityRoles { get; set; }

        public string FullName { get; set; }


        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public List<Filial> Filials { get; set; }

        public int FilialId { get; set; }
    }
}
