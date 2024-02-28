using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
  public class AppUser:IdentityUser<string>
    {

        public string FullName { get; set; }

        public string ForgetPasswordCode  { get; set;}

        public bool IsBlock { get; set; } = false;

        public Filial Filial { get; set;}

        public int FilialId { get; set;}

        public  List<LazerAppointment> LazerAppointments { get; set;}

        public List<SolariumAppointment> SolariumAppointments { get;set;}

        public List<BodyshapingAppointment> BodyshapingAppointments { get; set; }

        public List<SolariumUsingList> SolariumUsingList { get; set; }

        public List<BodyShapingSessionList> BodyshapingSessionList { get; set; }


        public List<CosmetologyAppointment> CosmetologyAppointments { get; set; }

    }
}
