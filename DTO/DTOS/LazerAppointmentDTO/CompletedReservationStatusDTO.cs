using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }

        public string Description { get; set; }

        public int LazerMasterId { get; set; }

        public bool IsCompleted { get; set; }   
    }
}
