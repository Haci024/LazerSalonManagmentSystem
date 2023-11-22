using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class LazerAppointmentReports
    {
       public int Id { get; set; }

        public LazerCategory LazerCategory { get; set; }

        public int LazerCategoryId { get; set; }

        public LazerAppointment LazerAppointment { get; set; }

        public int LazerAppointmentId { get; set; }

       
    }
}
