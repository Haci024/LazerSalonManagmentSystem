using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LazerAppointmentDTO
{
    public class CompletelySecondSessionEndDTO
    {

        public DateTime? InCompleteEnd { get; set; }

        public bool Succesfully { get; set; }

        public string CustomerFullName { get; set; }

        public string Lazeroloq { get; set; }

        public int ImpulsCount { get; set; }
    }
}
