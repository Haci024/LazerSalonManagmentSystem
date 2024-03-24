using Business.IServices;
using Data.Concrete;
using DTO.DTOS.LazerAppointmentDTO;
using DTO.DTOS.ReportDTO;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LazerBeautyFullProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,SuperSupporter,Supporter")]
    public class EditReportsController : Controller
    {
        private readonly ICosmetologyAppointmentService _service;
        private readonly ICosmetologService _cosmetolog;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _appUser;
        private readonly ILazerAppointmentService _lazerService;
        public EditReportsController(ICosmetologyAppointmentService service,UserManager<AppUser> user,ICosmetologService cosmetolog,ILazerAppointmentService lazerService,AppDbContext dbContext)
        {
            _service = service;
            _cosmetolog = cosmetolog;
            _appDbContext = dbContext;
            _lazerService= lazerService;
            _appUser = user;
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
        [HttpGet]
        public IActionResult ReservationListForUpdate()
        {

            List<LazerAppointment> lazerAppointments=_appDbContext.LazerAppointments.Include(x=>x.Customers).Include(x=>x.Filial).Include(x=>x.AppUser).Include(x=>x.LazerAppointmentReports).ThenInclude(x=>x.LazerCategory).Include(x=>x.LazerMaster).Where(x=>x.IsCompleted==true).ToList();
           
            return View(lazerAppointments);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateLazerInfo(int AppointmentId)
        {
            LazerAppointment lazerAppointment=await _lazerService.SelectLazerAppointment(AppointmentId);
            EditLazerInfoDTO editLazerInfoDTO = new EditLazerInfoDTO();
            editLazerInfoDTO.ReservationDate= lazerAppointment.ReservationDate;
            editLazerInfoDTO.Price= lazerAppointment.Price;
            
        
           


            return View(editLazerInfoDTO);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateLazerInfo(int AppointmentId, EditLazerInfoDTO editLazerInfoDTO)
        {
            AppUser AppUser = await _appUser.FindByNameAsync(User.Identity.Name);
            
            LazerAppointment lazerAppointment = await _lazerService.SelectLazerAppointment(AppointmentId);
            lazerAppointment.Price = editLazerInfoDTO.Price;
            lazerAppointment.StartTime = lazerAppointment.StartTime.Value.Date + editLazerInfoDTO.StartDate;
            lazerAppointment.EndTime = lazerAppointment.EndTime.Value.Date + editLazerInfoDTO.EndDate;
            lazerAppointment.Decription=editLazerInfoDTO.Description;
            lazerAppointment.EditorName = AppUser.UserName;
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Giriş və çıxış saatları yenidən daxil edilməlidir.Açıqlama boş ola bilməz!!!");
       

                return View(editLazerInfoDTO);  
            }
            _lazerService.Update(lazerAppointment);
            return RedirectToAction("ReservationListForUpdate", "EditReports");
        }
        





    }
}
