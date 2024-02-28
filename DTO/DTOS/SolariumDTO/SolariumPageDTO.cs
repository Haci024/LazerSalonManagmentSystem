using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.SolariumDTO
{
    public class SolariumPageDTO
    {
        public List<Customer> Customers { get; set; }

        public List<SolariumAppointment> ActivePackets { get; set; }

        public List<SolariumAppointment> TimeOutPackets { get; set; }

        public List<SolariumAppointment> SuccessfullyPackets { get; set; }

        public List<SolariumAppointment>  InjectionList { get; set; }


    }
}
