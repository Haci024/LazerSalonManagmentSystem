using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.PersonalDTO
{
    public class NewPersonalDTO
    {
        public int FilialId { get; set; }

        public string FullName { get; set; }

        public List<Filial> Filials { get; set; }

        public int[] filialId { get; set; } 
    }
}
