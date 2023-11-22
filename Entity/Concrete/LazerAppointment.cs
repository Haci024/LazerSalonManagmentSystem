using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class LazerAppointment
    {
        public int Id { get; set; }

        public DateTime ReservationDate { get; set; }
     
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? InCompleteStartTime { get; set; }

        public DateTime? InCompleteEndTime { get; set; }

        public Customer Customers { get; set; }

        public LazerMaster LazerMaster { get; set; }
      
        public int LazerMasterId { get; set; }

        public int CustomerId { get; set; }

        public decimal  Price { get; set; }

        public List<LazerAppointmentReports> LazerAppointmentReports { get; set; }

        public int  ImplusCount { get; set; }
     
        public bool IsStart { get; set; }

        public bool IsCompleted { get; set; }

        public string? Decription { get; set; }

        public string? PriceUpdateDescription { get; set; }

        public bool IsContiued { get; set; } = false;

        public DateTime? NextSessionDate { get; set; }

        public bool StartForSecond { get; set; } = false;

        public bool EndForSecond { get; set; } = false;

        public AppUser AppUser { get; set; }

        public string AppUserId { get; set; }

        public bool IsDeleted { get; set; }

        public Filial Filial { get; set; }

        public int FilialId { get; set; }


    }
}
