using Business.IServices;
using Data.Concrete;
using DTO.DTOS.LazerAppointmentDTO;
using DTO.DTOS.PersonalDTO;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Validation.ValidationRules.PersonalValidation;

namespace LazerBeautyFullProject.Areas.Support.Controllers
{
    [Area("Support")]
    [Authorize(Roles ="SuperSupporter,Supporter")]

    public class PersonalController : Controller
    {
        private readonly ILazerMasterService _masterService;
        private readonly ICosmetologService _cosmetologService;
        private readonly IBodyShapingMasterService _bodyShapingMasterService;
        private readonly ISolariumAppointmentService _solariumAppointmentService;
        private readonly IBodyShapingAppointmentService _bodyShapingAppointmentService; 
        private readonly ICosmetologyAppointmentService _cosmetologyAppointmentService;
        private readonly AppDbContext _appDbContext;
        private readonly ICustomerService _customerService;
        private readonly ILazerAppointmentService _lazerAppointmentService;
        public PersonalController(ICosmetologService cosmetologService,ICosmetologyAppointmentService cosmetologyAppointmentService,ISolariumAppointmentService solariumService,IBodyShapingAppointmentService bodyShapingAppointmentService,ICustomerService customerService,ILazerAppointmentService lazerAppointmentService,AppDbContext appDbContext,ILazerMasterService lazerMasterService,IBodyShapingMasterService bodyShapingMasterService)
        {
            _masterService = lazerMasterService;
            _cosmetologService = cosmetologService;
            _bodyShapingMasterService = bodyShapingMasterService;
            _appDbContext = appDbContext;
            _lazerAppointmentService = lazerAppointmentService;
            _solariumAppointmentService = solariumService;
            _customerService = customerService;
            _bodyShapingAppointmentService = bodyShapingAppointmentService;
            _cosmetologyAppointmentService= cosmetologyAppointmentService;
        }
        #region Lazeroloqlar
        [HttpGet]
        public async Task<IActionResult> LazeroloqList()
        {
            List<LazerMaster> lazerMasters =await _masterService.AllLazeroloq();
            return View(lazerMasters);
        }
        [HttpGet]
        public async Task<IActionResult> LazeroloqSessionList(int LazeroloqId)
        {
          LazerMaster lazerMaster=_masterService.GetById(LazeroloqId);
            ViewBag.LazeroloqName = lazerMaster.FullName;
            List<LazerAppointment> lazerAppointments=await _appDbContext.LazerAppointments.Include(x=>x.AppUser).Include(x=>x.Customers).Include(x=>x.LazerMaster).Include(x=>x.LazerAppointmentReports).ThenInclude(x=>x.LazerCategory).Where(x=>x.LazerMasterId==LazeroloqId).ToListAsync();
         
            return View(lazerAppointments);
        }
        [HttpGet]
        public IActionResult AddLazeroloq()
        {
            NewPersonalDTO lazerMasterDTO = new NewPersonalDTO();
            lazerMasterDTO.Filials=_appDbContext.Filials.Include(x=>x.LazerMasterFilials).ThenInclude(x=>x.LazerMaster).ToList();
            return View(lazerMasterDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddLazeroloq(NewPersonalDTO lazerMasterDTO)
        {
            lazerMasterDTO.Filials =await _appDbContext.Filials.Include(x => x.LazerMasterFilials).ThenInclude(x => x.LazerMaster).ToListAsync();
            var validator = new PersonalValidator();
            var validationResult = validator.Validate(lazerMasterDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(lazerMasterDTO);
            }
            LazerMaster lazeoloq=new LazerMaster();
            lazeoloq.FullName=lazerMasterDTO.FullName;
            List<LazerMasterFilial> lazerMasterFilials = new List<LazerMasterFilial>();

            foreach (var item in lazerMasterDTO.filialId)
            {
                LazerMasterFilial lazerMasterFilial = new LazerMasterFilial()
                {

                    FilialId = item,
                };
                lazerMasterFilials.Add(lazerMasterFilial);
            }
            lazeoloq.LazerMasterFilial = lazerMasterFilials;
            lazeoloq.IsDeactive = false;
            _masterService.Create(lazeoloq);
            return RedirectToAction("LazeroloqList","Personal");
        }
        [HttpGet]
        public IActionResult EditLazeroloq(int LazeroloqId)
        {
            LazerMaster lazerMaster=_masterService.GetById(LazeroloqId);
            EditPersonalDTO editPersonaldDTO = new EditPersonalDTO();
            editPersonaldDTO.FullName = lazerMaster.FullName;
            return View(editPersonaldDTO);
        }
        public  IActionResult DeleteSession(int AppointmentId)
        {
            LazerAppointment lazerAppointments =_lazerAppointmentService.GetById(AppointmentId);
                int LazeroloqId=_appDbContext.LazerAppointments.Where(x=>x.Id==AppointmentId).Select(x=>x.LazerMasterId).FirstOrDefault();  
            _lazerAppointmentService.Delete(lazerAppointments);
            return RedirectToAction("LazeroloqSessionList", "Personal",new{ LazeroloqId= LazeroloqId });
        }
        [HttpPost]
        public IActionResult EditLazeroloq(int LazeroloqId,EditPersonalDTO editPersonalDTO)
        {

            LazerMaster lazerMaster = _masterService.GetById(LazeroloqId);

            lazerMaster.FullName=editPersonalDTO.FullName;
            _masterService.Update(lazerMaster);
            return RedirectToAction("LazeroloqList", "Personal");
        }
    
        [HttpGet]
        public IActionResult LazeroloqOFF(int LazeroloqId)
        {
            LazerMaster lazerMaster = _masterService.GetById(LazeroloqId);
            if (lazerMaster.IsDeactive == false)
            {
                lazerMaster.IsDeactive = true;
                _masterService.Update(lazerMaster);

                return RedirectToAction("LazeroloqList", "Personal");
            }
            else
            {
                lazerMaster.IsDeactive = false;
                _masterService.Update(lazerMaster);
                return RedirectToAction("LazeroloqList", "Personal");
            }


        }
#endregion
        #region Kosmetoloqlar
        [HttpGet]
        public IActionResult CosmetoloqList()
        {
            List<Cosmetologs> cosmetologs = _appDbContext.Cosmetologs.Include(x => x.CosmetologsFilial).ThenInclude(x=>x.Filials).ToList();

            return View(cosmetologs);
        }
        [HttpGet]
        public async Task<IActionResult> AddCosmetoloq()
        {
            NewPersonalDTO lazerMasterDTO = new NewPersonalDTO();
            lazerMasterDTO.Filials =await _appDbContext.Filials.Include(x => x.LazerMasterFilials).ThenInclude(x=>x.LazerMaster).Include(x=>x.CosmetologFilial).ThenInclude(x=>x.Cosmetologs).Include(x => x.BodyShapingMasters).ToListAsync();

            return View(lazerMasterDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddCosmetoloq(NewPersonalDTO cosmetoloqDTO)
        {
            cosmetoloqDTO.Filials = await _appDbContext.Filials.Include(x => x.CosmetologFilial).ThenInclude(x=>x.Cosmetologs).Include(x => x.BodyShapingMasters).ToListAsync();
            var validator = new PersonalValidator();
            var validationResult=validator.Validate(cosmetoloqDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(cosmetoloqDTO);
            }
         
            Cosmetologs cosmetolog = new Cosmetologs();
            cosmetolog.FullName = cosmetoloqDTO.FullName;
            List<CosmetologFilial> cosmetologFilials = new List<CosmetologFilial>();

            foreach (var item in cosmetoloqDTO.filialId)
            {
                CosmetologFilial CosmetoloqFilial = new CosmetologFilial()
                {

                    FilialId = item,
                };
                cosmetologFilials.Add(CosmetoloqFilial);
            };

            cosmetolog.CosmetologsFilial = cosmetologFilials;
            cosmetolog.IsDeactive = false;
            _cosmetologService.Create(cosmetolog);
            return RedirectToAction("CosmetoloqList", "Personal");
        }
        [HttpGet]
        public IActionResult EditCosmetoloq(int CosmetoloqId)
        {
            Cosmetologs Cosmetoloq = _cosmetologService.GetById(CosmetoloqId);
            EditPersonalDTO editPersonaldDTO = new EditPersonalDTO();
            editPersonaldDTO.FullName = Cosmetoloq.FullName;
            return View(editPersonaldDTO);
        }
        [HttpPost]
        public IActionResult EditCosmetoloq(int CosmetoloqId, EditPersonalDTO editPersonalDTO)
        {
            Cosmetologs Cosmetoloq = _cosmetologService.GetById(CosmetoloqId);
            Cosmetoloq.FullName = editPersonalDTO.FullName;
            _cosmetologService.Update(Cosmetoloq);
            return RedirectToAction("CosmetoloqList", "Personal");
        }
  
        [HttpGet]
        public IActionResult CosmetoloqOFF(int CosmetoloqId)
        {
            Cosmetologs cosmetolog = _cosmetologService.GetById(CosmetoloqId);
            if (cosmetolog.IsDeactive == false)
            {
                cosmetolog.IsDeactive = true;

                _cosmetologService.Update(cosmetolog);
                return RedirectToAction("CosmetoloqList", "Personal");
            }
            else
            {
                cosmetolog.IsDeactive = false;
                _cosmetologService.Update(cosmetolog);
                return RedirectToAction("CosmetoloqList", "Personal");
            }
        }
        #endregion
        #region Bədən Şəkilləndirmə ustaları
        [HttpGet]
        public IActionResult BodyShapingMasterList()
        {
            List<BodyShapingMaster> bodyShapingMaster = _appDbContext.BodyShapingMasters.Include(x => x.Filial).ToList();

            return View(bodyShapingMaster);
        }

        [HttpGet]
        public async Task<IActionResult> AddBodyShapingMaster()
        {
            NewPersonalDTO lazerMasterDTO = new NewPersonalDTO();
            lazerMasterDTO.Filials = await _appDbContext.Filials.Include(x => x.LazerMasterFilials).ThenInclude(x=>x.LazerMaster).Include(x => x.BodyShapingMasters).ToListAsync();

            return View(lazerMasterDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddBodyShapingMaster(NewPersonalDTO bodyShapingMasterDTO)
        {
            bodyShapingMasterDTO.Filials = await _appDbContext.Filials.Include(x => x.LazerMasterFilials).ThenInclude(x=>x.LazerMaster).Include(x => x.BodyShapingMasters).ToListAsync();
            var validator = new PersonalValidator();
            var validationResult = validator.Validate(bodyShapingMasterDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(bodyShapingMasterDTO);
            }
            BodyShapingMaster bodyShapingMaster = new BodyShapingMaster();
            bodyShapingMaster.FullName = bodyShapingMasterDTO.FullName;
            bodyShapingMaster.FilialId = bodyShapingMasterDTO.FilialId;
            bodyShapingMaster.IsDeactive = false;
            _bodyShapingMasterService.Create(bodyShapingMaster);
            return RedirectToAction("BodyShapingMasterList", "Personal");
        }
        [HttpGet]
        public IActionResult EditBodyShapingMaster(int BodyShapingMasterId)
        {
            BodyShapingMaster bodyShapingMaster = _bodyShapingMasterService.GetById(BodyShapingMasterId);
            EditPersonalDTO editPersonaldDTO = new EditPersonalDTO();
            editPersonaldDTO.FullName = bodyShapingMaster.FullName;
            return View(editPersonaldDTO);
        }
        [HttpPost]
        public IActionResult EditBodyShapingMaster(int BodyShapingMasterId, EditPersonalDTO editPersonalDTO)
        {
            BodyShapingMaster bodyShapingMaster =_bodyShapingMasterService.GetById(BodyShapingMasterId);
            bodyShapingMaster.FullName = editPersonalDTO.FullName;
            _bodyShapingMasterService.Update(bodyShapingMaster);
            return RedirectToAction("BodyShapingMasterList", "Personal");
        }
  
        [HttpGet]
        public IActionResult BodyShapingMasterOFF(int BodyShapingMasterId)
        {
            BodyShapingMaster bodyShappingMaster = _bodyShapingMasterService.GetById(BodyShapingMasterId);
            if (bodyShappingMaster.IsDeactive == false)
            {
                bodyShappingMaster.IsDeactive = true;
               
                _bodyShapingMasterService.Update(bodyShappingMaster);
                return RedirectToAction("BodyShapingMasterList", "Personal");
            }
            else
            {
                bodyShappingMaster.IsDeactive = false;
               _bodyShapingMasterService.Update(bodyShappingMaster);


                return RedirectToAction("BodyShapingMasterList", "Personal");
            }
         

        }
        #endregion
        [HttpGet]
        public IActionResult AllCustomers()
        {
            List<Customer> customers = _appDbContext.Customers.Include(x=>x.Filial).ToList();
            return View(customers);
        }
        [HttpGet]
        public IActionResult CustomerOFF(int CustomerId)
        {
           
            Customer customer = _customerService.GetById(CustomerId);
            if (customer.IsDeactive==false)
            {
                customer.IsDeactive = true;
                _customerService.Update(customer);
                return RedirectToAction("AllCustomers", "Personal");
            }
            customer.IsDeactive =false;
            _customerService.Update(customer);           
            return RedirectToAction("AllCustomers","Personal");
        }


    }
}
