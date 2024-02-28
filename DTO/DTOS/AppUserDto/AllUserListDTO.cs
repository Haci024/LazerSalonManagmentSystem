using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.AppUserDto
{
    public class AllUserListDTO
    {
        public  List<IdentityRole> Roles { get; set; }
        public List<AppUser> Users { get; set; }
    }
}
