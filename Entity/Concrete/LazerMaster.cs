using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class LazerMaster

    {
        public int  Id { get; set; }

        public string FullName { get; set; }

        public List<LazerAppointment> LazerAppointment { get; set; }  
        
        public List<LazerMasterFilial> LazerMasterFilial { get; set; }

        public bool IsDeactive { get; set; }

    }
}
