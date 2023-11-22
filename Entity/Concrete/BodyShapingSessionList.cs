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

        public DateTime? SessionDate { get; set; }

        public string SessionName { get; set; }

        public bool IsCompleted { get; set; } = false;



    }
}
