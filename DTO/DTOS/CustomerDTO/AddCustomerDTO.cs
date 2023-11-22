using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.CustomerDTO
{
    public class AddCustomerDTO
    {
        public DateTime BirthDate { get; set; }

        public string  FullName { get; set; }

       public bool IsFemale { get; set; }   

        public double PhoneNumber { get; set; }

    
    }
}
