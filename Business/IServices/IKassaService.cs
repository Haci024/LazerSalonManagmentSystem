using DTO.DTOS.ReportDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IKassaService
    {
        public decimal Budget(int FilialId);
       
        public DailyReportDTO GetDailyReport(int FilialId,DailyReportDTO dailyReportDTO);

       
    }
}
