using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.BodyShapingDTO
{
    public class UpdatePriceDTO
    {
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int BodyShapingMasterId {  get; set; }   

      
    }
}
