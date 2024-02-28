using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.ReportDTO
{
    public class DailyReportDTO
    {
        public decimal Budget { get; set; }

        public List<LazerMaster> Lazeroloqs{get;set;}

        public List<BodyShapingMaster> BodyShapingMasters { get;set;}

        public List<Cosmetologs> Cosmetologs { get; set; }

        public List<AppUser> AppUsers { get; set; }

        public List<LazerAppointment> LazerAppointments { get; set; }
        
        public decimal ShopingEarning { get; set; }

        public decimal OutMoneyFromKassa { get; set; }

        public decimal DailyLazerEarning { get; set; }

        public decimal DailySolariumEarning { get; set; }

        public decimal DailyBodyShapingEarning { get; set; }

        public decimal DailyCosmetologyEarning { get; set; }

        public int ImplusCount { get; set; }

        public decimal TotalEarning { get; set; }

        public decimal Spending { get; set; }

        public decimal LipuckaEarning { get; set; }

        public decimal PirsinqEarning { get; set; }


    }
}
