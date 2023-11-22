using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.OutMoneyDTO
{
    public class UpdateOutMoneyDTO
    {
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Product { get; set; }
    }
}
