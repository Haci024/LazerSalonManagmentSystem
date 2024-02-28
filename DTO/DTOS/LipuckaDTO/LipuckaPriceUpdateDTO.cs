using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LipuckaDTO
{
    public class LipuckaPriceUpdateDTO
    {
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int LipuckaMasterId { get; set; }
    }
}
