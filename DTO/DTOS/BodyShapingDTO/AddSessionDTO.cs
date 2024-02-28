using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.BodyShapingDTO
{
    public class AddSessionDTO
    {
        public string PacketName { get; set; }

        public int Duration {  get; set; }

        public int SessionCount { get; set;}

        public int AppointmentId { get; set; }


    }
}
