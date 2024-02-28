using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class BodyShapingSessionList
    {
        public int Id { get; set; }

        public  BodyshapingAppointment  BodyShapingAppointment{ get; set; }

        public int BodyShapingAppointmentId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Duration { get; set; }

        public string SessionName { get; set; }

        public bool IsCompleted { get; set; } = false;

        public AppUser AppUser { get; set; }   
       
        public string? AppUserId {  get; set; }



    }
}
