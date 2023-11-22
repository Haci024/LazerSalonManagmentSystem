using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.CustomerDTO
{
    public class CustomerUpdateDTO
    {
        public double PhoneNumber { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
