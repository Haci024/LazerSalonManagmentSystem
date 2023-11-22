using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.OutMoneyDTO
{
    public class OutMoneyAddDTO
    {
       
        public string Product { get; set; } 

        public Decimal Price { get; set; }

        public string Description { get; set; }
    }
}
