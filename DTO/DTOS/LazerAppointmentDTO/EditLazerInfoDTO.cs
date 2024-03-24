using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LazerAppointmentDTO
{
    public class EditLazerInfoDTO
    {
        public DateTime ReservationDate { get; set; }   

        public TimeSpan? StartDate { get; set; }

        public TimeSpan? EndDate { get; set; }


        public Decimal Price { get; set; }

        public string Description { get; set; }


    }
}
