using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.PirsinqDTO
{
    public class PirsinqPriceUpdateDTO
    {
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int PirsinqMasterId { get; set; }    

    }
}
