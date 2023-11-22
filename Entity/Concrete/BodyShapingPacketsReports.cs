using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class BodyShapingPacketsReports
    {
        public int Id { get; set; }

        public BodyshapingAppointment BodyshapingAppointments { get; set; }

        public int BodyshapingAppointmentsId { get; set; }

        public BodyShapingPacketCategory BodyShapingPackets { get; set; }

        public int BodyShapingPacketCategoryId { get; set; }
    }
}
