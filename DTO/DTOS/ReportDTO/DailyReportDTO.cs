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

        public decimal DailyTotalInComeMoney { get; set; }

        public decimal DailyTotalOutMoney { get; set; }

        public decimal DailyBenefitMoney { get; set; }

        public decimal Master1DailyEarnings { get; set; }

        public decimal Master2DailyEarnings { get; set; }

        public decimal ProceedsFromSales { get; set; }

        public int ImplusCount { get; set; }
    }
}
