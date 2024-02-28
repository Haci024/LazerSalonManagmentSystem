using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class BodyShapingMaster
    {
        public int Id { get; set; }

        public string FullName { get; set; }  
        
        public bool  IsDeactive { get; set; }

        public List<BodyshapingAppointment> BodyShapingAppointment { get; set; }    

        public Filial Filial { get; set; }

        public int FilialId { get; set; }

        


    }
}
