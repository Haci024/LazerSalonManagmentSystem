using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.InComeDTO
{
    public class UpdateInComeDTO
    {
      

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }
    }
}
