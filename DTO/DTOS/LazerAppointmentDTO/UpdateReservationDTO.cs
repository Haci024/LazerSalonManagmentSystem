using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LazerAppointmentDTO
{
    public class UpdateReservationDTO
    {
        


        public string CustomerName { get; set; }

        public string LazerMasterName { get; set; }
      
        public DateTime NewReservationDate { get; set; }

        public bool IsStart { get; set; }

        public DateTime EnterTime { get; set; }

        public DateTime? StartTime { get; set; }

        public string Description { get; set; }

        public int LazerMasterId { get; set; }

    }
}
