using Business.IServices;
using DTO.DTOS.BodyShapingDTO;
using Data.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Validation.ValidationRules.BodyShapingValidator;


namespace   LazerBeautyFullProject.Areas.ArzumEstetic.Controllers
{
    [Area("ArzumEstetic")]
    [Authorize]
    public class BodyShapingAppointmentController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IBodyShapingPacketCategoriesService _category;
        private readonly IBodyShapingAppointmentService _appointment;
        private readonly IBodyShapingPacketsReportsService _Reports;
        private readonly IBodyShapingSessionListService _sessionList;
        private readonly TimeHelper _timeHelper;
        private readonly ICustomerService _customer;
       
        private readonly IBodyShapingMasterService _master;
        private readonly UserManager<AppUser> _userManager;
        public BodyShapingAppointmentController(AppDbContext db,UserManager<AppUser> userManager,IBodyShapingMasterService bodyShapingMasterService,ICustomerService customer,IBodyShapingAppointmentService appointment,IBodyShapingPacketCategoriesService category,IBodyShapingPacketsReportsService reports,IBodyShapingSessionListService usinglist)
        {
            _db = db;
            _category=category;
            _appointment=appointment;
            _Reports=reports;
            _sessionList=usinglist;
            _customer = customer;
            _master = bodyShapingMasterService;
            _userManager=userManager;
          
            _timeHelper=new TimeHelper();
        }
     
        [HttpGet]
        public async Task<IActionResult> ComboPacket(int CustomerId,int BodyShapingMasterId)
        {
            var Customer = _customer.GetById(CustomerId);
            var master=_master.GetById(BodyShapingMasterId);
           
            BodyShapingComboDTO bodyShapingComboDTO=new BodyShapingComboDTO();
           bodyShapingComboDTO.BodyShapingMasterId=BodyShapingMasterId;
            bodyShapingComboDTO.BodyShapingPacketCategories =await _db.BodyShapingPacketCategories.Include(x => x.MainCategory).Where(x => x.Id != 1 && x.Id != 2 && x.Id != 3 && x.MainCategoryId == null && x.IsDeactive==false).ToListAsync();
            bodyShapingComboDTO.CustomerName=Customer.FullName;
            bodyShapingComboDTO.Master = master.FullName;
            bodyShapingComboDTO.Price = 0;
           
            
            return View(bodyShapingComboDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComboPacket(int CustomerId,int BodyShapingMasterId,BodyShapingComboDTO bodyShapingComboDTO)
        {
            BodyShapingMaster master = _master.GetById(BodyShapingMasterId);
            Customer customer = _customer.GetById(CustomerId);
            bodyShapingComboDTO.BodyShapingPacketCategories = await _db.BodyShapingPacketCategories.Include(x => x.MainCategory).Where(x =>x.Id != 1 && x.Id!=2 && x.Id!=3 && x.MainCategoryId==null && x.IsDeactive==false).ToListAsync();

            ComboPacketValidator validationRules = new ComboPacketValidator();
            var validation = validationRules.Validate(bodyShapingComboDTO);
            if (!validation.IsValid)
            {
                foreach (var item in validation.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);

                }
                return View(bodyShapingComboDTO);
            }
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            BodyshapingAppointment bodyshapingAppointment=new BodyshapingAppointment();
            bodyshapingAppointment.CustomerId = CustomerId;
            bodyshapingAppointment.BuyingDate = _timeHelper.ConvertToAzerbaijanTime(DateTime.Now); 
            bodyshapingAppointment.BodyshapingMasterId=master.Id;
            bodyshapingAppointment.AppUserId = appUser.Id;
            bodyshapingAppointment.FilialId = 3;
            bodyshapingAppointment.ReturnMoney = 0;
            bodyshapingAppointment.Price =(decimal)bodyShapingComboDTO.Price;
           
            bodyshapingAppointment.RemaingDate = _timeHelper.ConvertToAzerbaijanTime(DateTime.Now.AddDays(30));
           
               List<BodyShapingPacketsReports> bodyShapingPacketsReports = new List<BodyShapingPacketsReports>();
          
                BodyShapingPacketsReports bodyShapingPacketsReport = new BodyShapingPacketsReports()
                {
                    BodyShapingPacketCategoryId = bodyShapingComboDTO.CategoryId,
                };
                 bodyShapingPacketsReports.Add(bodyShapingPacketsReport);
             
            bodyshapingAppointment.BodyShapingPacketReports = bodyShapingPacketsReports;
            
                 _appointment.Create(bodyshapingAppointment);
         
            return RedirectToAction("BodyShapingMasterPage",new {BodyShapingMasterId=master.Id});
        }
        [HttpGet]
        public IActionResult BodyShapingMasterPage(int BodyShapingMasterId)
        {
            ViewBag.BodyShapingMasterId = BodyShapingMasterId;
            BodyShapingMaster Master=_master.GetById(BodyShapingMasterId);
            ViewBag.BodyShapingMaster = Master.FullName;
            BodyShapingPageDTO bodyShapingPageDTO = new BodyShapingPageDTO();
           
            
            bodyShapingPageDTO.BodyShapingMaster = Master.FullName;
            bodyShapingPageDTO.Customers= _db.Customers.Where(x=>x.Female==true).ToList();
            bodyShapingPageDTO.ActivePackets=_db.BodyShapingAppointments.Include(x=>x.Customer).
                Include(x=>x.BodyShapingSessionLists).Include(x=>x.BodyShapingPacketReports).
                ThenInclude(x=>x.BodyShapingPackets).ThenInclude(x=>x.MainCategory).
                Where(x=>x.IsCompleted==false && x.FilialId==3 && x.BodyshapingMasterId==BodyShapingMasterId).ToList();
              
            bodyShapingPageDTO.SuccessfullyPackets = _db.BodyShapingAppointments.Include(x => x.Customer).
              Include(x => x.BodyShapingSessionLists).Include(x => x.BodyShapingPacketReports).
              ThenInclude(x => x.BodyShapingPackets).ThenInclude(x => x.MainCategory).Where(x=>x.IsCompleted==true && x.FilialId==3 && x.BodyshapingMasterId==BodyShapingMasterId).
              ToList();
            bodyShapingPageDTO.InjectionPackets = _db.BodyShapingAppointments.Include(x => x.Customer).
              Include(x => x.BodyShapingSessionLists).Include(x => x.BodyShapingPacketReports).
              ThenInclude(x => x.BodyShapingPackets).ThenInclude(x => x.MainCategory).
              Where(x=>x.IsCompleted==true && x.IsDeactive==true && x.FilialId==3 && x.BodyshapingMasterId == BodyShapingMasterId).
              ToList();

            return View(bodyShapingPageDTO);
        }
        [HttpGet]
        public IActionResult AddBodyShapingAppointment(int BodyShapingMasterId,int CustomerId) {
           
           ViewBag.BodyShapingMasterId = BodyShapingMasterId;
       Customer customer=_customer.GetById(CustomerId);
       BodyShapingMaster Master = _master.GetById(BodyShapingMasterId);
       AddBodyShapingAppointmentDTO addBodyShapingAppointmentDTO = new AddBodyShapingAppointmentDTO();
       addBodyShapingAppointmentDTO.BodyShapingMaster = Master.FullName;
       addBodyShapingAppointmentDTO.BodyShapingMasterId = 1;
       addBodyShapingAppointmentDTO.FullName = customer.FullName;
       addBodyShapingAppointmentDTO.ChildCategories = _db.BodyShapingPacketCategories.Include(x => x.ChildCategory).Include(x=>x.MainCategory).Where(x => x.MainCategoryId!=null && x.IsDeactive==false).ToList();
          

            return View(addBodyShapingAppointmentDTO);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBodyShapingAppointment(int BodyShapingMasterId,int CustomerId,AddBodyShapingAppointmentDTO ModelDTO) {

            ViewBag.BodyShapingMasterId=BodyShapingMasterId;
            ModelDTO.ChildCategories = _db.BodyShapingPacketCategories.Include(x => x.ChildCategory).Where(x=>x.IsDeactive==false && ModelDTO.ChildCategoryId.Contains(x.Id)).ToList();
            AddBodyShapingAppointmentValidator validationRules = new AddBodyShapingAppointmentValidator();
             var validation=validationRules.Validate(ModelDTO);
            if (!validation.IsValid)
            {
                foreach (var item in validation.Errors)
                {
                    ModelState.AddModelError("",item.ErrorMessage);

                   
                }
                return View(ModelDTO);
            }
            BodyShapingMaster BodyShapingMaster= _master.GetById(BodyShapingMasterId);
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
           BodyshapingAppointment bodyshapingAppointment =new BodyshapingAppointment();
            bodyshapingAppointment.CustomerId=CustomerId;
            bodyshapingAppointment.BuyingDate=_timeHelper.GetAzerbaijanTime();
            bodyshapingAppointment.Price=ModelDTO.Price;
            bodyshapingAppointment.AppUserId = appUser.Id;
            bodyshapingAppointment.FilialId = 3;
            bodyshapingAppointment.CustomerId = CustomerId;
            bodyshapingAppointment.ReturnMoney = 0;
            bodyshapingAppointment.RemaingDate = _timeHelper.ConvertToAzerbaijanTime(DateTime.Now.AddDays(30));
            bodyshapingAppointment.BodyshapingMasterId= BodyShapingMasterId;
            List<BodyShapingPacketsReports> reports= new List<BodyShapingPacketsReports>();
          
            foreach(int childcategories in ModelDTO.ChildCategoryId)
            {
                BodyShapingPacketsReports bodyShapingPacketsReports = new BodyShapingPacketsReports
                {
                    BodyShapingPacketCategoryId = childcategories,
                };
                reports.Add(bodyShapingPacketsReports);
            }
            bodyshapingAppointment.BodyShapingPacketReports = reports;
            _appointment.Create(bodyshapingAppointment);

            foreach (var item in ModelDTO.ChildCategories)
            {
                for (int session = 1; session <= item.SessionCount; session++)
                {
                    BodyShapingSessionList sessions = new BodyShapingSessionList
                    {
                        SessionName = item.Packet,
                        BodyShapingAppointmentId = bodyshapingAppointment.Id,
                        Duration = (int)item.SessionDuration,
                        IsCompleted = false,
                        AppUserId=appUser.Id
                    };
                    _db.BodyShapingSessionList.Add(sessions);
                }
            } 
            _db.SaveChanges();
           
          
            return RedirectToAction("BodyShapingMasterPage",new { BodyShapingMasterId= BodyShapingMaster.Id });
        }
        [HttpGet]
        public async Task<IActionResult> SessionList(int BodyShapingMasterId,int BodyShapingAppointmentId)
        {
            ViewBag.AppointmentId = BodyShapingAppointmentId;
            var Master = _master.GetById(BodyShapingMasterId);
            SessionListDTO sessionListDTO = new SessionListDTO();
            sessionListDTO.SessionLists =await _db.BodyShapingSessionList.Include(x=>x.AppUser).Include(x=>x.BodyShapingAppointment).ThenInclude(x=>x.BodyShapingPacketReports).ThenInclude(x=>x.BodyShapingPackets).ThenInclude(x=>x.ChildCategory).Where(x=>x.BodyShapingAppointmentId== BodyShapingAppointmentId).ToListAsync();
            BodyshapingAppointment bodyshapingAppointment =await _db.BodyShapingAppointments.Include(x => x.BodyShapingPacketReports).ThenInclude(x => x.BodyShapingPackets).ThenInclude(x => x.MainCategory).Where(x => x.Id == BodyShapingAppointmentId).FirstOrDefaultAsync();
   
            ViewBag.BodyShapingMasterId = bodyshapingAppointment.BodyshapingMasterId;
            return View(sessionListDTO);
        }
        [HttpGet]
        public IActionResult EditSession(int SessionId)
        {
           
            BodyShapingSessionList bodyShapingSessionList = _sessionList.GetById(SessionId);
            
            NewSessionDTO newSessionDTO = new NewSessionDTO();
           
            newSessionDTO.SessionId = bodyShapingSessionList.BodyShapingAppointmentId;

            return View(newSessionDTO);
        }
    
            [HttpPost]
            public async Task<IActionResult> EditSession(int SessionId, NewSessionDTO newSessionDTO)
            {
               
                BodyShapingSessionList session = await _db.BodyShapingSessionList.Include(x => x.BodyShapingAppointment).ThenInclude(x => x.Customer).Where(x => x.Id == SessionId).FirstOrDefaultAsync();
                BodyshapingAppointment appointment = _db.BodyShapingAppointments.Include(x => x.BodyShapingMaster).Where(x => x.Id == session.BodyShapingAppointmentId).FirstOrDefault();
                ViewBag.BodyShapingMasterId = appointment.BodyshapingMasterId;
            newSessionDTO.SessionId = session.BodyShapingAppointmentId;
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                int lastSessionId = _db.BodyShapingSessionList.Include(x => x.BodyShapingAppointment)
                       .Where(x => x.BodyShapingAppointmentId == session.BodyShapingAppointmentId).OrderBy(x => x.Id).
                      Select(x => x.Id).LastOrDefault();
                var validator = new NewSessionValidator();
                var validationresult = validator.Validate(newSessionDTO);
                if (!validationresult.IsValid)
                {
                    foreach (var item in validationresult.Errors)
                    {
                        ModelState.AddModelError("", item.ErrorMessage);
                    }
                    return View(newSessionDTO);

                }
                if (newSessionDTO.Duration > session.Duration)
                {
                    ModelState.AddModelError("", "Qeyd edilmiş müddət seans müddətindən çoxdur!");

                    return View(newSessionDTO);
                }
                if (session.Id == lastSessionId)
                {
                    session.StartDate = newSessionDTO.StartDate;
                    session.EndDate = newSessionDTO.EndDate;
                    session.AppUserId = appUser.Id;
                    if (newSessionDTO.Duration < session.Duration)
                    {
                        ModelState.AddModelError("", "Bu seans sonuncudur! Buna görə tam istifadə edilməlidir və ya tam qeyd edilməlidir!");
                        return View(newSessionDTO);
                    }
                    session.Duration = newSessionDTO.Duration;
                    session.IsCompleted = true;
                    _sessionList.Update(session);
                    appointment.IsCompleted = true;
                    _appointment.Update(appointment);
                    return RedirectToAction("SessionList", new { BodyShapingAppointmentId = session.BodyShapingAppointmentId });
                }
                else
                {
                    session.StartDate = newSessionDTO.StartDate;
                    session.EndDate = newSessionDTO.EndDate;
                    session.AppUserId = appUser.Id;
                    if (newSessionDTO.Duration < session.Duration)
                    {
                        BodyShapingSessionList nextSession = _db.BodyShapingSessionList.FirstOrDefault(x => x.Id == SessionId + 1);
                        int addTime = session.Duration - newSessionDTO.Duration;
                        session.Duration = session.Duration - addTime;
                        nextSession.Duration = nextSession.Duration + addTime;
                        session.IsCompleted = true;
                        _sessionList.Update(session);
                        _sessionList.Update(nextSession);
                        return RedirectToAction("SessionList", new { BodyShapingAppointmentId = session.BodyShapingAppointmentId });
                    }
                    session.Duration = newSessionDTO.Duration;
                    session.IsCompleted = true;
                    _sessionList.Update(session);
                    return RedirectToAction("SessionList", new { BodyShapingAppointmentId = session.BodyShapingAppointmentId });

                }
            
        }
        [HttpGet]
        public IActionResult AddG8TurboSessions(int AppointmentId)
        {
            BodyshapingAppointment bodyshapingAppointment=_appointment.GetById(AppointmentId);
            AddSessionDTO addSessionDTO = new AddSessionDTO();
            addSessionDTO.PacketName = "G8 turbo ";
            addSessionDTO.AppointmentId = AppointmentId;
             
            return View(addSessionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddG8TurboSessions(int AppointmentId,AddSessionDTO addSessionDTO)
        {
            AppUser AppUser =await _userManager.FindByNameAsync(User.Identity.Name);
            addSessionDTO.AppointmentId=AppointmentId;
            BodyshapingAppointment bodyshapingAppointment = _appointment.GetById(AppointmentId);
            var validator = new AddSessionValidator();
            var validationresult = validator.Validate(addSessionDTO);
            if (!validationresult.IsValid)
            {
                foreach (var item in validationresult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(addSessionDTO);

            }

            for (int session = 1; session <= addSessionDTO.SessionCount; session++)
            {
                BodyShapingSessionList sessions = new BodyShapingSessionList
                {
                    SessionName = addSessionDTO.PacketName,
                    BodyShapingAppointmentId = bodyshapingAppointment.Id,
                    Duration = addSessionDTO.Duration,
                    IsCompleted = false,
                    AppUserId=AppUser.Id
                };
                _db.BodyShapingSessionList.Add(sessions);
            }
            _db.SaveChanges();
            
            return RedirectToAction("SessionList", new { BodyShapingAppointmentId= AppointmentId});
        }
        [HttpGet]
        public IActionResult AddMioStimulyasiyaSession(int AppointmentId)
        {
            BodyshapingAppointment bodyshapingAppointment = _appointment.GetById(AppointmentId);
            AddSessionDTO addSessionDTO = new AddSessionDTO();
            addSessionDTO.PacketName = "MioStimulyasiya";
            addSessionDTO.AppointmentId = AppointmentId;

            return View(addSessionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddMioStimulyasiyaSession(int AppointmentId, AddSessionDTO addSessionDTO)
        {
            addSessionDTO.AppointmentId = AppointmentId;
            AppUser AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            BodyshapingAppointment bodyshapingAppointment = _appointment.GetById(AppointmentId);
            var validator = new AddSessionValidator();
            var validationresult = validator.Validate(addSessionDTO);
            if (!validationresult.IsValid)
            {
                foreach (var item in validationresult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(addSessionDTO);

            }

            for (int session = 1; session <= addSessionDTO.SessionCount; session++)
            {
                BodyShapingSessionList sessions = new BodyShapingSessionList
                {
                    SessionName = addSessionDTO.PacketName,
                    BodyShapingAppointmentId = bodyshapingAppointment.Id,
                    Duration = addSessionDTO.Duration,
                    IsCompleted = false,
                    AppUserId=AppUser.Id
                };
                _db.BodyShapingSessionList.Add(sessions);
            }
            _db.SaveChanges();

            return RedirectToAction("SessionList", new { BodyShapingAppointmentId = AppointmentId });
        }
        [HttpGet]
        public IActionResult AddTermoYorganSession(int AppointmentId)
        {
            BodyshapingAppointment bodyshapingAppointment = _appointment.GetById(AppointmentId);
            AddSessionDTO addSessionDTO = new AddSessionDTO();
            addSessionDTO.PacketName = "Termo yorğan";
            addSessionDTO.AppointmentId= AppointmentId;
            return View(addSessionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddTermoYorganSession(int AppointmentId, AddSessionDTO addSessionDTO)
        {
            AppUser AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            addSessionDTO.AppointmentId = AppointmentId;
            BodyshapingAppointment bodyshapingAppointment = _appointment.GetById(AppointmentId);
            var validator = new AddSessionValidator();
            var validationresult = validator.Validate(addSessionDTO);
            if (!validationresult.IsValid)
            {
                foreach (var item in validationresult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(addSessionDTO);

            }

            for (int session = 1; session <= addSessionDTO.SessionCount; session++)
            {
                BodyShapingSessionList sessions = new BodyShapingSessionList
                {
                    SessionName = addSessionDTO.PacketName,
                    BodyShapingAppointmentId = bodyshapingAppointment.Id,
                    Duration = addSessionDTO.Duration,
                    IsCompleted = false,
                    AppUserId=AppUser.Id
                };
                _db.BodyShapingSessionList.Add(sessions);
            }
            _db.SaveChanges();

            return RedirectToAction("SessionList", new { BodyShapingAppointmentId = AppointmentId });
        }
        [HttpGet]
        public IActionResult UpdatePrice(int AppointmentId)
        {
            BodyshapingAppointment bodyshapingAppointment= _appointment.GetById(AppointmentId);

            UpdatePriceDTO updatePriceDTO = new UpdatePriceDTO();
            updatePriceDTO.BodyShapingMasterId = bodyshapingAppointment.BodyshapingMasterId;
            updatePriceDTO.Price = bodyshapingAppointment.Price;
            updatePriceDTO.Description= bodyshapingAppointment.Description;


            return View(updatePriceDTO);
        }
        [HttpPost]
        public IActionResult UpdatePrice(int AppointmentId,UpdatePriceDTO updatePriceDTO)
        {
            BodyshapingAppointment bodyshapingAppointment = _appointment.GetById(AppointmentId);
            updatePriceDTO.BodyShapingMasterId=bodyshapingAppointment.BodyshapingMasterId;
            var validator = new UpdatePriceValidator();
            var validationresult = validator.Validate(updatePriceDTO);
            if (!validationresult.IsValid)
            {
                foreach (var item in validationresult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(updatePriceDTO);

            }
            
            
            if (bodyshapingAppointment.Price < updatePriceDTO.Price)
            {
                
                bodyshapingAppointment.Price = updatePriceDTO.Price;
                bodyshapingAppointment.Description= updatePriceDTO.Description;
            }
            else
            {
                
                bodyshapingAppointment.Price = updatePriceDTO.Price;
                bodyshapingAppointment.Description = updatePriceDTO.Description;
            }
         
            _appointment.Update(bodyshapingAppointment);
            return RedirectToAction("BodyShapingMasterPage", new { BodyShapingMasterId = bodyshapingAppointment.BodyshapingMasterId });
        }
        [HttpGet]
        public IActionResult InjectionPacket(int AppointmentId)
        {
            BodyshapingAppointment bodyshapingAppointment = _appointment.GetById(AppointmentId);
            InjectionPacketDTO injectionPacket=new InjectionPacketDTO();
            injectionPacket.PacketValue = bodyshapingAppointment.Price;
            injectionPacket.Description=bodyshapingAppointment.Description;
            injectionPacket.ReturnMoney = 0;
            injectionPacket.MasterId = bodyshapingAppointment.BodyshapingMasterId;
            return View(injectionPacket);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InjectionPacket(int AppointmentId, InjectionPacketDTO injectionPacket)
        {
           
            BodyshapingAppointment bodyshapingAppointment = _appointment.GetById(AppointmentId);
            injectionPacket.MasterId = bodyshapingAppointment.BodyshapingMasterId;
            if (injectionPacket.Description == null)
            {
                ModelState.AddModelError("", "Açıqlama olmadan paketdən imtina edilə bilməz!");

                return View(injectionPacket);
            }
            bodyshapingAppointment.Description=injectionPacket.Description;
            bodyshapingAppointment.ReturnMoney = injectionPacket.ReturnMoney;
            bodyshapingAppointment.Price = bodyshapingAppointment.Price - injectionPacket.ReturnMoney;
            bodyshapingAppointment.IsDeactive = true;
            bodyshapingAppointment.IsCompleted = true;
         
           _appointment.Update(bodyshapingAppointment); 
            
            return RedirectToAction("BodyShapingMasterPage", new { BodyShapingMasterId = bodyshapingAppointment.BodyshapingMasterId});
        }


    }
}
