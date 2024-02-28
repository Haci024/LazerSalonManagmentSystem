using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class PirsinqAppointment
    {
        public int Id { get; set; } 

        public Customer Customer { get; set; }  

        public int CustomerId { get; set; }

        public LazerMaster LazerMaster { get; set; }

        public int LazerMasterId { get;set; }

        public AppUser AppUser { get; set; }

        public string AppUserId { get; set; }

        public decimal Price { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }   

        public bool IsDeactive { get; set; }

        public bool IsStart { get; set; }

        public bool IsCompleted { get; set; }

        public Filial Filial { get; set; }

        public int FilialId { get; set; }

        public string? Description { get; set; }

        public List<PirsinqReports> PirsinqReports { get;set; }

    }
}
