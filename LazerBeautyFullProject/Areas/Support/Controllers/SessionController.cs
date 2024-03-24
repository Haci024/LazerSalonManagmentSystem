using Business.IServices;
using Data.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LazerBeautyFullProject.Areas.Support.Controllers
{
    [Area("Support")]
    [Authorize(Roles = "SuperSupporter,Supporter")]
    public class SessionController : Controller
    {
       
        private readonly ISolariumAppointmentService _solariumAppointmentService;
        private readonly IBodyShapingAppointmentService _bodyShapingAppointmentService;
        private readonly ICosmetologyAppointmentService _cosmetologyAppointmentService;
        private readonly AppDbContext _db;
        private readonly IOutMoneyService _outMoneyService;
        private readonly IPirsinqAppointmentService _pirsinqAppointmentService;
        private readonly ILipuckaAppointmentService _lipuckaAppointmentService;
        private readonly ILazerAppointmentService _lazerAppointmentService;
        public SessionController(ICosmetologService cosmetologService,IOutMoneyService outMoneyService,ILipuckaAppointmentService lipuckaAppointmentService,IPirsinqAppointmentService pirsinqAppointmentService, ICosmetologyAppointmentService cosmetologyAppointmentService, ISolariumAppointmentService solariumService, IBodyShapingAppointmentService bodyShapingAppointmentService, ICustomerService customerService, ILazerAppointmentService lazerAppointmentService, AppDbContext appDbContext, ILazerMasterService lazerMasterService, IBodyShapingMasterService bodyShapingMasterService)
        {
          
            _db = appDbContext;
            _lazerAppointmentService = lazerAppointmentService;
            _solariumAppointmentService = solariumService;
            _bodyShapingAppointmentService = bodyShapingAppointmentService;
            _cosmetologyAppointmentService = cosmetologyAppointmentService;
            _lipuckaAppointmentService= lipuckaAppointmentService;
            _pirsinqAppointmentService = pirsinqAppointmentService;
            _outMoneyService = outMoneyService;
        }

        [HttpGet]
        public IActionResult LipuckaSessionList()
        {
            List<LipuckaAppointment> appointment =_db.LipuckaAppointments.Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x => x.Customer).Include(x => x.LipuckaReports).ThenInclude(x => x.LipuckaCategories).ThenInclude(x=>x.MainCategory).ToList();
            return View(appointment);
        }
        [HttpGet]
        public IActionResult PirsinqSessionList()
        {
            List<PirsinqAppointment> appointment = _db.PirsinqAppointments.Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x => x.Customer).Include(x => x.PirsinqReports).ThenInclude(x => x.PirsinqCategory).ThenInclude(x=>x.MainCategory).ToList();
            return View(appointment);
        }
        [HttpGet]
        public IActionResult CosmetologyAppointmentList()
        {
            List<CosmetologyAppointment> appointment = _db.CosmetologyAppointments.Include(x => x.Cosmetolog).Include(x => x.AppUser).Include(x => x.Customers).Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory).ThenInclude(x=>x.MainCategory).ToList();
            return View(appointment);
        }
        [HttpGet]
        public IActionResult SolariumSessions()
        {
            List<SolariumAppointment> solariumAppointments = _db.SolariumAppointments.Include(x => x.Customer).Include(x => x.SolariumCategories).Include(x => x.SolariumUsingList).Include(x => x.AppUser).Include(x => x.Filial).ToList();

            return View(solariumAppointments);
        }
        [HttpGet]
        public IActionResult BodyShapingAppointmentList()
        {
            List<BodyshapingAppointment> appointment = _db.BodyShapingAppointments.Include(x => x.Customer).Include(x => x.BodyShapingPacketReports).ThenInclude(x=>x.BodyShapingPackets).ThenInclude(x=>x.MainCategory).Include(x => x.BodyShapingMaster).Include(x => x.AppUser).Include(x => x.Filial).ToList();

            return View(appointment);
        }
        [HttpGet]
        public IActionResult DeleteSolariumSession(int SolariumId)
        {

            SolariumAppointment solariumAppointment = _solariumAppointmentService.GetById(SolariumId);
            _solariumAppointmentService.Delete(solariumAppointment);
            return RedirectToAction("SolariumSessions", "Session");

        }
        [HttpGet]
        public IActionResult DeleteBodyShapingAppointment(int PacketId)
        {

            BodyshapingAppointment appointment = _bodyShapingAppointmentService.GetById(PacketId);
            _bodyShapingAppointmentService.Delete(appointment);
            return RedirectToAction("BodyShapingAppointmentList", "Session");

        }
        [HttpGet]
        public IActionResult DeleteCosmetologyAppointment(int AppointmentId)
        {

            CosmetologyAppointment appointment = _cosmetologyAppointmentService.GetById(AppointmentId);
            _cosmetologyAppointmentService.Delete(appointment);
            return RedirectToAction("CosmetologyAppointmentList", "Session");

        }
        [HttpGet]
        public IActionResult DeletePirsinqSession(int AppointmentId)
        {

            PirsinqAppointment appointment = _pirsinqAppointmentService.GetById(AppointmentId);
            _pirsinqAppointmentService.Delete(appointment);
            return RedirectToAction("PirsinqSessionList", "Session");

        }
        [HttpGet]
        public IActionResult DeleteLipuckaSession(int AppointmentId)
        {

            LipuckaAppointment appointment = _lipuckaAppointmentService.GetById(AppointmentId);
            _lipuckaAppointmentService.Delete(appointment);
            return RedirectToAction("LipuckaSessionList", "Session");

        }
        [HttpGet]
        public IActionResult OutMoneyList()
        {
            
           IEnumerable<OutMoney> outMoney=_db.OutMoney.Include(x=>x.AppUser).Include(x=>x.SpendCategory).ThenInclude(x=>x.Filial).ToList();
            return View(outMoney);
        }
        [HttpGet]
        public IActionResult DeleteOutMoney(int Id) { 
        

        OutMoney outMoney=_outMoneyService.GetById(Id);
            _outMoneyService.Delete(outMoney);
       
            return RedirectToAction("OutMoneyList","Session");
        }

    }
}
