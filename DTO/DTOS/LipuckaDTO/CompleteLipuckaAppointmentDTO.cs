using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LipuckaDTO
{
    public class CompleteLipuckaAppointmentDTO
    {
        public string Customer {  get; set; }   

        public string Master {  get; set; }

        public decimal Price { get; set; }

        public int LipuckaMasterId { get;set; }

        public string Description { get; set; }
       
        public DateTime? EndTime { get; set; }


    }
}
