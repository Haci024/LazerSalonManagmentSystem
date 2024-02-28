using Business.IServices;
using Data.Concrete;
using DTO.DTOS.ReportDTO;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LazerBeautyFullProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,SuperSupporter,Supporter")]
    public class CosmetologyReportsController : Controller
    {
        private readonly ICosmetologyAppointmentService _service;
        private readonly ICosmetologService _cosmetolog;
        private readonly AppDbContext _appDbContext;
        public CosmetologyReportsController(ICosmetologyAppointmentService service,ICosmetologService cosmetolog,AppDbContext dbContext)
        {
            _service = service;
            _cosmetolog = cosmetolog;
            _appDbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> FilterCosmetologReports()
        {
            SelectFilterDTO filters = new SelectFilterDTO();
            filters.Cosmetologs = _appDbContext.Cosmetologs.ToList();
            filters.Filials = _appDbContext.Filials.Where(x => x.Id!=4 && x.Id!=5 && x.Id!=1).ToList();
            filters.CosmetologyCategories = await _appDbContext.CosmetologyCategories.Include(x => x.ChildCategory).Include(x => x.MainCategory).Where(x => x.MainCategoryId != null).ToListAsync();

            return View(filters);
        }
        [HttpPost]
        public JsonResult GetCosmetologyPrice(DateTime startDate, DateTime endDate,int[] filialId,int[] cosmetologId ,int[] categoryId)
        {
            var AllEarnings= CalculateEarningsForAllFilial(startDate, endDate, filialId, cosmetologId, categoryId);
            var AllSession= CalculateSessionForAllFilial(startDate, endDate,filialId,cosmetologId, categoryId);
            var totalFilialEarnings = CalculateAllPrice(startDate, endDate);
            var totalSessionCount= CalculateAllSessionCount(startDate, endDate);


            var response = new
            {

                totalEarnings= AllEarnings,
                totalSessionCount = AllSession,
                totalFilialEarnings = totalFilialEarnings,
                totalSessionCountForAllFilial = totalSessionCount

            };
            return Json(response);
        }
        public decimal CalculateEarningsForAllFilial(DateTime startDate, DateTime endDate,int[] filialId ,int[] CosmetologId, int[] cosmetologyCategoriesId)
        {
            IQueryable<CosmetologyAppointment> cosmetologyAppointments = _appDbContext.CosmetologyAppointments
                .Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory)
                .Where(x => x.OutTime >= startDate  && filialId.Contains(x.FilialId)
                         && x.OutTime <= endDate && CosmetologId.Contains(x.CosmetologId) && x.IsCompleted==true
                         && x.CosmetologyReports.Any(cr => cosmetologyCategoriesId.Contains(cr.CosmetologyCategoryId)));

            decimal totalPrice = cosmetologyAppointments.Sum(x => x.Price);
          
            return totalPrice;
        }
        public int CalculateSessionForAllFilial(DateTime startDate, DateTime endDate, int[] filialId,int[] CosmetologId,int[] cosmetologyCategories)
        {
            IQueryable<CosmetologyAppointment> cosmetologyAppointments = _appDbContext.CosmetologyAppointments
               .Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory)
               .Where(x => x.OutTime >= startDate && filialId.Contains(x.FilialId)
                        && x.OutTime <= endDate && x.IsCompleted == true && CosmetologId.Contains(x.CosmetologId) && x.IsCompleted == true
                         && x.CosmetologyReports.Any(cr => cosmetologyCategories.Contains(cr.CosmetologyCategoryId)));
            int totalSessionCount = cosmetologyAppointments.Count();

            return totalSessionCount;
        }


        public int CalculateAllSessionCount(DateTime startDate, DateTime endDate)
        {
            IQueryable<CosmetologyAppointment> cosmetologyAppointments = _appDbContext.CosmetologyAppointments
                 .Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory)
                 .Where(x => x.OutTime >= startDate && x.OutTime <= endDate && x.IsCompleted == true);

            int totalSessionCount = cosmetologyAppointments.Count();

            return totalSessionCount;
        }
        public decimal CalculateAllPrice(DateTime startDate, DateTime endDate)
        {
            IQueryable<CosmetologyAppointment> cosmetologyAppointments = _appDbContext.CosmetologyAppointments
                .Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory)
                .Where(x => x.OutTime >= startDate 
                         && x.OutTime <= endDate && x.IsCompleted==true);

            decimal totalPrice = cosmetologyAppointments.Sum(x => x.Price);

            return totalPrice;
        }


        [HttpPost] 
        public PartialViewResult SelectCosmetologyReportsAll(DateTime startDate, DateTime endDate)
        {
            List<CosmetologyAppointment> cosmetologyAppointments = SelectCosmetologyListDate(startDate, endDate);

            return PartialView("~/Areas/Admin/Views/Shared/_CosmetologyReports.cshtml", cosmetologyAppointments);


        }
        public List<CosmetologyAppointment> SelectCosmetologyListDate(DateTime startDate, DateTime endDate)
        {
            List<CosmetologyAppointment> cosmetologyAppointments = _appDbContext.CosmetologyAppointments
               .Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory).Include(x => x.Cosmetolog).Include(x => x.AppUser).Include(x => x.Customers)
               .Where(x => x.OutTime >= startDate
      && x.OutTime <= endDate && x.IsCompleted == true).ToList();


            return cosmetologyAppointments;
        }

        [HttpPost]
        public PartialViewResult SelectCosmetologyListForAllFilter(DateTime startDate, DateTime endDate, int[] filialId, int[] CosmetologId, int[] cosmetologyCategoriesId)
        {
            List<CosmetologyAppointment> cosmetologyAppointments = SelectCosmetologyListForAllFilters(startDate, endDate,filialId,CosmetologId,cosmetologyCategoriesId);

            return PartialView("~/Areas/Admin/Views/Shared/_CosmetologyReports.cshtml", cosmetologyAppointments);


        }
        public List<CosmetologyAppointment> SelectCosmetologyListForAllFilters(DateTime startDate, DateTime endDate, int[] filialId, int[] CosmetologId, int[] cosmetologyCategoriesId)
        {
            List<CosmetologyAppointment> cosmetologyAppointments = _appDbContext.CosmetologyAppointments
                .Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory).Include(x=>x.AppUser).Include(x=>x.Cosmetolog).Include(x=>x.Customers)
                .Where(x => x.OutTime >= startDate && filialId.Contains(x.FilialId)
                         && x.OutTime <= endDate && CosmetologId.Contains(x.CosmetologId) && x.IsCompleted == true
                         && x.CosmetologyReports.Any(cr => cosmetologyCategoriesId.Contains(cr.CosmetologyCategoryId))).ToList();

           

            return cosmetologyAppointments;
        }







    }
}
