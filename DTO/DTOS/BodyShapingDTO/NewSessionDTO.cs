using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.BodyShapingDTO
{
    public class NewSessionDTO
    {
        public bool IsCompleted { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int SessionId { get; set; }

        public int Duration { get; set; }

        public int AppointmentId { get; set; }  





    }
}
