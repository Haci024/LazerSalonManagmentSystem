﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public  class Filial
    {
        public int Id { get; set; }

        public string FilialName { get; set; }

        public List<LazerAppointment> LazerAppointments { get; set; } 

        public List<CosmetologFilial> CosmetologFilial { get; set; }

         public List<BodyShapingMaster> BodyShapingMasters { get; set; }

        public List<AppUser> AppUser { get; set; }

        public List<SolariumAppointment> SolariumAppointments { get; set; }

        public List<CosmetologyAppointment> CosmetologyAppointments { get; set; }

        public List<BodyshapingAppointment> BodyShapingAppointments { get; set; }

        public List<LazerMasterFilial>  LazerMasterFilials { get; set;}

        public List<KassaActionList> KassaActionList { get; set;}

        public List<Income> Income { get; set; }

        public List<Stock> Stock { get; set; }

        public List<SpendCategory> SpendCategory { get; set; }

        public List<Customer> Customer { get; set; }

        public List<LazerCategory> LazerCategories  { get; set; }

        public List<LipuckaAppointment> LipuckaAppointments { get; set; }

        public List<PirsinqAppointment> PirsinqAppointments { get; set; }

    }
}
