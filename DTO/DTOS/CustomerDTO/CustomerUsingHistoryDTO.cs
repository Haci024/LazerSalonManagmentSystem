
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.CustomerDTO
{
    public class CustomerUsingHistoryDTO
    {

        public List<LazerAppointment> LazerAppointmentsHistory { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public List<CosmetologyAppointment> CosmetologyAppointments { get; set; }

        public List<SolariumAppointment> SolariumAppointments { get; set; }

        public List<BodyshapingAppointment> BodyshapingAppointments { get; set; }

        public List<PirsinqAppointment> PirsinqAppointments { get; set; }

        public List<LipuckaAppointment> LipuckaAppointments { get; set; }

    }
}
