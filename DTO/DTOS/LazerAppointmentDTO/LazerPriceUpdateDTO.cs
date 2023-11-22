using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LazerAppointmentDTO
{
    public class LazerPriceUpdateDTO
    {
        public decimal Price { get; set; }

        public string Desciption { get; set; }

        public int LazerMasterId { get; set; }
    }
}
