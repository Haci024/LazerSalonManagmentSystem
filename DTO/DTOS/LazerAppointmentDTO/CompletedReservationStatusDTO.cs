using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LazerAppointmentDTO
{
    public class CompletedReservationStatusDTO
    {
        
        public int RequestId { get; set; }

        public int ImpulsCount { get; set; }

        public string CustomerName { get; set; }

        public string LazerMasterName { get; set; }

        public DateTime? EndTime { get; set; }

        public string Description { get; set; }

        public int LazerMasterId { get; set; }

        public bool IsCompleted { get; set; }   
    }
}
