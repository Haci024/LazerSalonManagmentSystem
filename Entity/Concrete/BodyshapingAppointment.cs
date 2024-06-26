﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class BodyshapingAppointment
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public decimal Price { get; set; }

        public DateTime BuyingDate { get; set; }

        public List<BodyShapingSessionList> BodyShapingSessionLists { get; set; }

        public List<BodyShapingPacketsReports> BodyShapingPacketReports { get; set; }

        public Filial Filial { get; set; }

        public int FilialId { get; set; }

        public BodyShapingMaster BodyShapingMaster { get; set; }

        public int BodyshapingMasterId { get; set;}

        public AppUser AppUser { get; set; }

        public DateTime RemaingDate { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsDeactive { get; set; }

        public string AppUserId { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal ReturnMoney { get; set; } = 0;
    }
}
