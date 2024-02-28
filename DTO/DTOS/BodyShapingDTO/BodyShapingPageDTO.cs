using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.BodyShapingDTO
{
    public class BodyShapingPageDTO
    {
        public List<BodyshapingAppointment> ActivePackets { get; set; }
        public List<BodyshapingAppointment> SuccessfullyPackets { get; set; }
        public List<BodyshapingAppointment> InjectionPackets { get; set; }
        public List<Customer> Customers { get; set; }
        public string BodyShapingMaster { get; set; }
        public int BodyShapingMasterId { get;set; } 
        
    }
}
