using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class PirsinqReports
    {
        public int Id { get; set; }    

        public PirsinqAppointment PirsinqAppointment { get; set; }
       
        public int PirsinqAppointmentId { get; set; }

        public PirsinqCategory PirsinqCategory { get; set;}

        public int PirsinqCategoryId { get;set; }


    }
}
