using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Customer
    {
        public int Id { get; set; }

        public List<LazerAppointment> LazerAppointments { get; set; }

        public List<SolariumAppointment> SolariumAppointments { get; set; }

        public List<BodyshapingAppointment> BodyshapingAppointments { get; set; }

        public DateTime BirthDate { get; set; }

        public string FullName { get; set; }

        public double PhoneNumber { get; set; }

        public bool Female { get; set; }

        public Filial Filial { get; set; }

        public int FilialId { get; set; }

        public bool IsDeactive { get;set; }

    }
}
