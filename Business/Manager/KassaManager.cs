using Business.IServices;
using Data.Concrete;
using DTO.DTOS.ReportDTO;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager
{
    public class KassaManager : IKassaService
    {
        private readonly AppDbContext _appDbContext;
        public KassaManager(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;

        }
        public decimal Budget(int FilialId)
        {
            List<Income> incomes = _appDbContext.Incomes.Where(x => x.FilialId == FilialId).ToList();
            List<OutMoney> outMoneys = _appDbContext.OutMoney.Include(x=>x.SpendCategory).Where(x => x.SpendCategory.FilialId == FilialId).ToList();
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Where(x => x.FilialId == FilialId && x.IsCompleted == true).ToList();
            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Where(x => x.FilialId == FilialId && x.IsCompleted == true).ToList();
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.FilialId == FilialId && x.IsCompleted == true).ToList();
            List<KassaActionList> KassaActionList = _appDbContext.KassaActionLists.Include(x => x.Filial).Where(x => x.FilialId == FilialId).ToList();
            List<SolariumAppointment> solariumAppointments = _appDbContext.SolariumAppointments.Include(x => x.Filial).Where(x => x.FilialId == FilialId).ToList();
            List<BodyshapingAppointment> bodyshapingAppointments = _appDbContext.BodyShapingAppointments.Include(x => x.Filial).Where(x => x.FilialId == FilialId).ToList();
            List<CosmetologyAppointment> cosmetologyAppointments = _appDbContext.CosmetologyAppointments.Include(x => x.Filial).Where(x => x.FilialId == FilialId).ToList();
            decimal TotalImcome = incomes.Where(x => x.IncomeDate.Date <= DateTime.Now.Date).Sum(x => x.Price);
            decimal TotalCosmetologyEarning = cosmetologyAppointments.Where(x => x.OutTime.Value.Date <= DateTime.Now.Date).Sum(x => x.Price);
            decimal TotalSolariumEarning = solariumAppointments.Where(x => x.BuyingDate.Date <= DateTime.Now.Date).Sum(x => x.Price);
            decimal TotalBodyShapingEarning = bodyshapingAppointments.Where(x => x.BuyingDate.Date <= DateTime.Now.Date).Sum(x => x.Price);
            decimal TotalLazerEarning = lazerAppointments.Where(x => x.EndTime.Value.Date <= DateTime.Now).Sum(x => x.Price);
            decimal TotalPirsinqEarning = pirsinqAppointments.Where(x => x.EndTime.Value.Date <= DateTime.Now.Date).Sum(x => x.Price);
            decimal TotalLipuckaEarning = lipuckaAppointments.Where(x => x.EndTime.Value.Date <= DateTime.Now.Date).Sum(x => x.Price);
            decimal TotalKassaOutMoney = KassaActionList.Where(x => x.Status == false && x.LastOutMoneyDate.Date <= DateTime.Now.Date).Sum(x => x.OutMoneyQuantity);
            decimal TotalKassaAddMoney = KassaActionList.Where(x => x.Status == true && x.LastOutMoneyDate.Date <= DateTime.Now.Date).Sum(x => x.OutMoneyQuantity);
            decimal TotalOutMoney = outMoneys.Where(x => x.AddingDate.Date <= DateTime.Now.Date).Sum(x => x.Price);
            decimal Budget = TotalImcome + TotalLazerEarning + TotalCosmetologyEarning + TotalKassaAddMoney + TotalSolariumEarning + TotalLipuckaEarning + TotalBodyShapingEarning + TotalPirsinqEarning - TotalKassaOutMoney - TotalOutMoney;
            return Budget;
        }

        public DailyReportDTO GetDailyReport(int FilialId, DailyReportDTO dailyReportDTO)
        {
            List<Income> incomes = _appDbContext.Incomes.Where(x => x.FilialId == FilialId).ToList();
            List<OutMoney> outMoneys = _appDbContext.OutMoney.Include(x => x.SpendCategory).Where(x => x.SpendCategory.FilialId == FilialId).ToList();
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Where(x => x.FilialId == FilialId && x.IsCompleted == true).ToList();
            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Where(x => x.FilialId == FilialId && x.IsCompleted == true).ToList();
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.FilialId == FilialId && x.IsCompleted == true).ToList();
            List<SolariumAppointment> solariumAppointments = _appDbContext.SolariumAppointments.Include(x => x.Filial).Where(x => x.FilialId == FilialId).ToList();
            List<BodyshapingAppointment> bodyshapingAppointments = _appDbContext.BodyShapingAppointments.Include(x => x.Filial).Where(x => x.FilialId == FilialId).ToList();
            List<CosmetologyAppointment> cosmetologyAppointments = _appDbContext.CosmetologyAppointments.Include(x => x.Filial).Where(x => x.FilialId == FilialId && x.IsCompleted == true).ToList();
            List<KassaActionList> KassaActionList = _appDbContext.KassaActionLists.Include(x => x.Filial).Where(x => x.FilialId == FilialId).ToList();

            decimal DailyLazerEarning = lazerAppointments.Where(x => x.EndTime.Value.Date == DateTime.Today.Date).Sum(x => x.Price);
            decimal DailyCosmetologyEarning = cosmetologyAppointments.Where(x => x.OutTime.Value.Date == DateTime.Today.Date).Sum(x => x.Price);
            decimal DailySolariumEarning = solariumAppointments.Where(x => x.BuyingDate.Date == DateTime.Today.Date).Sum(x => x.Price);
            decimal DailyBodyShapingEarning = bodyshapingAppointments.Where(x => x.BuyingDate.Date == DateTime.Today.Date).Sum(x => x.Price);
            decimal DailyLipuckaEarning = lipuckaAppointments.Where(x => x.EndTime.Value.Date == DateTime.Today).Sum(x => x.Price);
            decimal DailyPirsinqEarning = pirsinqAppointments.Where(x => x.EndTime.Value.Date == DateTime.Today.Date).Sum(x => x.Price);
            decimal DailyIncome = incomes.Where(x => x.IncomeDate.Date == DateTime.Today.Date).Sum(x => x.Price);
            decimal DailyOutMoney = outMoneys.Where(x => x.AddingDate.Date == DateTime.Today.Date).Sum(x => x.Price);
            decimal DailyOutMoneyFromKassa = KassaActionList.Where(x => x.LastOutMoneyDate.Date == DateTime.Today && x.Status == false).Sum(x => x.OutMoneyQuantity);
            decimal DailyAddMoneyFromKassa = KassaActionList.Where(x => x.LastOutMoneyDate.Date == DateTime.Today && x.Status == true).Sum(x => x.OutMoneyQuantity);
            int SumDailyImpulsCount = lazerAppointments.Where(x => x.EndTime.Value.Date == DateTime.Today).Sum(x => x.ImplusCount);

            decimal DailyTotalBenefit = DailyLazerEarning + DailySolariumEarning + DailyCosmetologyEarning + DailyIncome + DailyBodyShapingEarning + DailyLipuckaEarning + DailyPirsinqEarning - DailyOutMoney;
            dailyReportDTO.DailyLazerEarning = DailyLazerEarning;
            dailyReportDTO.PirsinqEarning = DailyPirsinqEarning;
            dailyReportDTO.LipuckaEarning = DailyLipuckaEarning;
            dailyReportDTO.DailySolariumEarning = DailySolariumEarning;
            dailyReportDTO.DailyBodyShapingEarning = DailyBodyShapingEarning;
            dailyReportDTO.DailyCosmetologyEarning = DailyCosmetologyEarning;
            dailyReportDTO.Spending = DailyOutMoney;
            dailyReportDTO.ShopingEarning = DailyIncome;
            dailyReportDTO.OutMoneyFromKassa = DailyOutMoneyFromKassa;
            dailyReportDTO.ImplusCount = SumDailyImpulsCount;


            dailyReportDTO.TotalEarning = DailyLazerEarning + DailyPirsinqEarning + DailyLipuckaEarning + DailyIncome + DailyCosmetologyEarning + DailySolariumEarning + DailyBodyShapingEarning - DailyOutMoney;

            return dailyReportDTO;
        }
    }
}
