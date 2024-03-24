using Business.IServices;
using Data.Concrete;
using DTO.DTOS.BodyShapingDTO;
using DTO.DTOS.CosmetologyDTO;
using DTO.DTOS.LazerAppointmentDTO;
using DTO.DTOS.LipuckaDTO;
using DTO.DTOS.PirsinqDTO;
using DTO.DTOS.SolariumDTO;
using Entity.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Validation.ValidationRules.BodyShapingValidator;
using Validation.ValidationRules.CosmetologyValidator;
using Validation.ValidationRules.LazerValidator;
using Validation.ValidationRules.LipuckaValidator;
using Validation.ValidationRules.PersonalValidation;
using Validation.ValidationRules.PirsinqValidator;
using Validation.ValidationRules.SolariumValidator;

namespace LazerBeautyFullProject.Areas.Support.Controllers
{

    [Area("Support")]
    [Authorize(Roles = "SuperSupporter,Supporter")]
    public class CategoriesController : Controller
    {
        private readonly ISolariumCategoryService _SolariumService;
        private readonly ILipuckaCategoriesService _lipuckaCategoriesService;
        private readonly AppDbContext _db;
        private readonly IPirsinqCategoriesService _pirsinqCategoriesService;
        private readonly IBodyShapingPacketCategoriesService _bodyShapingCategoryService;
        private readonly ILazerCategoryService _lazerCategoryService;
       
        private readonly ICosmetologCategoryService _cosmetologCategoryService;
        public CategoriesController(ISolariumCategoryService solariumCategories,IPirsinqCategoriesService pirsinqCategoriesService,ILipuckaCategoriesService lipuckaCategories,ILazerCategoryService lazerCategoryService ,ICosmetologCategoryService cosmetologCategoryService, AppDbContext db, IBodyShapingPacketCategoriesService bodyShapingCategoryService)
        {
            _SolariumService = solariumCategories;
            _db = db;
            _bodyShapingCategoryService = bodyShapingCategoryService;
            _cosmetologCategoryService = cosmetologCategoryService;
            _lazerCategoryService = lazerCategoryService;
            _lipuckaCategoriesService = lipuckaCategories;
            _pirsinqCategoriesService= pirsinqCategoriesService;

        }
        #region Solarium Paketləri
        [HttpGet]
        public IActionResult SolariumPacketList()
        {
            List<SolariumCategories> solariumCategories = _db.SolariumCategories.Include(x => x.MainCategory).Include(x => x.ChildCategories).Where(x => x.MainCategoryId != null).ToList();
            return View(solariumCategories);
        }
        [HttpGet]
        public IActionResult AddNewSolariumPacket()
        {
            NewSolariumPacketDTO solariumPacketDTO = new NewSolariumPacketDTO();
            solariumPacketDTO.MainCategories = _db.SolariumCategories.Where(x => x.MainCategoryId == null).ToList();

            return View(solariumPacketDTO);
        }
        [HttpPost]
        public IActionResult AddNewSolariumPacket(NewSolariumPacketDTO solariumPacketDTO)
        {
            solariumPacketDTO.MainCategories = _db.SolariumCategories.Where(x => x.MainCategoryId == null).ToList();
            SolariumCategories newCategory = new SolariumCategories();
            var validator = new SolariumPacketValidator();
            var validationResult = validator.Validate(solariumPacketDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(solariumPacketDTO);
            }
            newCategory.Price = solariumPacketDTO.Price;
            newCategory.MainCategoryId = solariumPacketDTO.MainCategoryId;
            newCategory.UsingPeriod = solariumPacketDTO.UsingPeriod;

            newCategory.Minute = solariumPacketDTO.Minute;
            newCategory.Name = solariumPacketDTO.CategoryName;
            newCategory.IsDeactive = false;

            _SolariumService.Create(newCategory);
            return RedirectToAction("SolariumPacketList", "Categories");
        }
        [HttpGet]
        public IActionResult UpdateSolariumPacket(int CategoryId)
        {
            SolariumCategories solariumCategories = _SolariumService.GetById(CategoryId);
            NewSolariumPacketDTO solariumPacketDTO = new NewSolariumPacketDTO();
            solariumPacketDTO.MainCategories = _db.SolariumCategories.Where(x => x.MainCategoryId == null).ToList();
            solariumPacketDTO.Price = (int)solariumCategories.Price;
            solariumPacketDTO.UsingPeriod = (int)solariumCategories.UsingPeriod;
            solariumPacketDTO.Minute = (int)solariumCategories.Minute;
            solariumPacketDTO.CategoryName = solariumCategories.Name;
            solariumPacketDTO.MainCategoryId = (int)solariumCategories.MainCategoryId;


            return View(solariumPacketDTO);
        }
        [HttpPost]
        public IActionResult UpdateSolariumPacket(int CategoryId, NewSolariumPacketDTO solariumPacketDTO)
        {
            solariumPacketDTO.MainCategories = _db.SolariumCategories.Where(x => x.MainCategoryId == null).ToList();
            SolariumCategories solariumCategories = _SolariumService.GetById(CategoryId);
            var validator = new SolariumPacketValidator();
            var validationResult = validator.Validate(solariumPacketDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(solariumPacketDTO);
            }
            solariumCategories.Price = solariumPacketDTO.Price;
            solariumCategories.MainCategoryId = solariumPacketDTO.MainCategoryId;
            solariumCategories.UsingPeriod = solariumPacketDTO.UsingPeriod;
            solariumCategories.Minute = solariumPacketDTO.Minute;
            solariumCategories.Name = solariumPacketDTO.CategoryName;

            _SolariumService.Update(solariumCategories);

            return RedirectToAction("SolariumPacketList", "Categories");
        }
        [HttpGet]
        public IActionResult PacketOFF(int CategoryId)
        {
            SolariumCategories packet = _SolariumService.GetById(CategoryId);
            if (packet.IsDeactive == false)
            {
                packet.IsDeactive = true;
                _SolariumService.Update(packet);

                return RedirectToAction("SolariumPacketList", "Categories");
            }
            else
            {
                packet.IsDeactive = false;
                _SolariumService.Update(packet);
                return RedirectToAction("SolariumPacketList", "Categories");
            }


        }

        #endregion
        #region Bədən Şəkilləndirmə Paketləri
        [HttpGet]
        public IActionResult BodyShapingPacketList()
        {
            List<BodyShapingPacketCategory> bodyShapingPacketCategories = _db.BodyShapingPacketCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory).Where(x => x.MainCategoryId != null).ToList();

            return View(bodyShapingPacketCategories);
        }
        [HttpGet]
        public IActionResult BodyShapingComboPacketList()
        {
            List<BodyShapingPacketCategory> bodyShapingPacketCategories = _db.BodyShapingPacketCategories.Include(x => x.MainCategory).Where(x => x.Id != 2 && x.Id != 8 && x.Id != 12 && x.MainCategoryId == null).ToList();

            return View(bodyShapingPacketCategories);
        }
        [HttpGet]
        public IActionResult AddNewBodyShapingPacket()
        {
            AddBodyShapingPacketDTO bodyShapingPacketDTO = new AddBodyShapingPacketDTO();
            bodyShapingPacketDTO.MainCategory = _db.BodyShapingPacketCategories.Where(x => x.MainCategoryId == null).ToList();

            return View(bodyShapingPacketDTO);
        }
        [HttpPost]
        public IActionResult AddNewBodyShapingPacket(AddBodyShapingPacketDTO bodyShapingPacketDTO)
        {
            bodyShapingPacketDTO.MainCategory = _db.BodyShapingPacketCategories.Include(x => x.MainCategory).Where(x => x.Id < 14 && x.MainCategoryId == null).ToList();
            BodyShapingPacketCategory bodyShapingPacketCategory = new BodyShapingPacketCategory();
            var validator = new CreateNewBodyShapingPacketValidator();
            var validationResult = validator.Validate(bodyShapingPacketDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(bodyShapingPacketDTO);
            }

            bodyShapingPacketCategory.SessionCount = bodyShapingPacketDTO.SessionCount;
            bodyShapingPacketCategory.Packet = bodyShapingPacketDTO.Packet;
            bodyShapingPacketCategory.SessionDuration = bodyShapingPacketDTO.SessionDuration;
            bodyShapingPacketCategory.Price = bodyShapingPacketDTO.Price;
            bodyShapingPacketCategory.MainCategoryId = bodyShapingPacketDTO.MainCategoryId;

            _bodyShapingCategoryService.Create(bodyShapingPacketCategory);
            return RedirectToAction("BodyShapingPacketList", "Categories");
        }
        [HttpGet]
        public IActionResult AddComboPacket()
        {
            NewComboPacketCategoryDTO newComboPacketCategoryDTO = new NewComboPacketCategoryDTO();

            return View(newComboPacketCategoryDTO);
        }
        [HttpPost]
        public IActionResult AddComboPacket(NewComboPacketCategoryDTO newComboPacketCategoryDTO)
        {

            BodyShapingPacketCategory bodyShapingPacketCategory = new BodyShapingPacketCategory();
            var validator = new AddComboPacketCategoryValidator();
            var validationResult = validator.Validate(newComboPacketCategoryDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(newComboPacketCategoryDTO);
            }
            bodyShapingPacketCategory.SessionCount = newComboPacketCategoryDTO.SessionCount;
            bodyShapingPacketCategory.Packet = newComboPacketCategoryDTO.PacketName;
            bodyShapingPacketCategory.SessionDuration = 0;
            bodyShapingPacketCategory.MainCategoryId = null;
            bodyShapingPacketCategory.Price = newComboPacketCategoryDTO.Price;

            _bodyShapingCategoryService.Create(bodyShapingPacketCategory);
            return RedirectToAction("BodyShapingComboPacketList", "Categories");
        }
        [HttpGet]
        public IActionResult BodyShapingPacketOFF(int CategoryId)
        {
            BodyShapingPacketCategory packet = _bodyShapingCategoryService.GetById(CategoryId);
            if (packet.IsDeactive == false)
            {
                packet.IsDeactive = true;
                _bodyShapingCategoryService.Update(packet);

                return RedirectToAction("BodyShapingPacketList", "Categories");
            }
            else
            {
                packet.IsDeactive = false;
                _bodyShapingCategoryService.Update(packet);
                return RedirectToAction("BodyShapingPacketList", "Categories");
            }


        }
        [HttpGet]
        public IActionResult BodyShapingComboPacketOFF(int CategoryId)
        {
            BodyShapingPacketCategory packet = _bodyShapingCategoryService.GetById(CategoryId);
            if (packet.IsDeactive == false)
            {
                packet.IsDeactive = true;
                _bodyShapingCategoryService.Update(packet);

                return RedirectToAction("BodyShapingComboPacketList", "Categories");
            }
            else
            {
                packet.IsDeactive = false;
                _bodyShapingCategoryService.Update(packet);
                return RedirectToAction("BodyShapingComboPacketList", "Categories");
            }


        }
        [HttpGet]
        public IActionResult UpdateBodyShapingPacket(int CategoryId)
        {
            BodyShapingPacketCategory bodyShapingPacketCategory = _bodyShapingCategoryService.GetById(CategoryId);
            AddBodyShapingPacketDTO bodyShapingPacketDTO = new AddBodyShapingPacketDTO();
            bodyShapingPacketDTO.MainCategory = _db.BodyShapingPacketCategories.Where(x => x.MainCategoryId == null).ToList();
            bodyShapingPacketDTO.SessionCount = (int)bodyShapingPacketCategory.SessionCount;
            bodyShapingPacketDTO.Price = (int)bodyShapingPacketCategory.Price;
            bodyShapingPacketDTO.Packet = bodyShapingPacketCategory.Packet;
            bodyShapingPacketDTO.SessionDuration = (int)bodyShapingPacketCategory.SessionDuration;
            bodyShapingPacketDTO.MainCategoryId = (int)bodyShapingPacketCategory.MainCategoryId;
            return View(bodyShapingPacketDTO);
        }
        [HttpPost]
        public IActionResult UpdateBodyShapingPacket(int CategoryId, AddBodyShapingPacketDTO bodyShapingPacketDTO)
        {
            bodyShapingPacketDTO.MainCategory = _db.BodyShapingPacketCategories.Where(x => x.MainCategoryId == null).ToList();
            BodyShapingPacketCategory bodyShapingPacketCategory = _bodyShapingCategoryService.GetById(CategoryId);
            var validator = new CreateNewBodyShapingPacketValidator();
            var validationResult = validator.Validate(bodyShapingPacketDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(bodyShapingPacketDTO);
            }
            bodyShapingPacketCategory.SessionCount = bodyShapingPacketDTO.SessionCount;
            bodyShapingPacketCategory.Packet = bodyShapingPacketDTO.Packet;
            bodyShapingPacketCategory.SessionDuration = bodyShapingPacketDTO.SessionDuration;
            bodyShapingPacketCategory.MainCategoryId = bodyShapingPacketDTO.MainCategoryId;
            bodyShapingPacketCategory.Price = bodyShapingPacketDTO.Price;
            _bodyShapingCategoryService.Update(bodyShapingPacketCategory);
            return RedirectToAction("BodyShapingPacketList", "Categories");
        }
        [HttpGet]
        public IActionResult UpdateComboPacket(int CategoryId)
        {
            NewComboPacketCategoryDTO bodyShapingPacketDTO = new NewComboPacketCategoryDTO();
            BodyShapingPacketCategory bodyShapingPacketCategory = _bodyShapingCategoryService.GetById(CategoryId);
            bodyShapingPacketDTO.PacketName = bodyShapingPacketCategory.Packet;
            bodyShapingPacketDTO.SessionCount = (int)bodyShapingPacketCategory.SessionCount;
            bodyShapingPacketDTO.Price = (decimal)bodyShapingPacketCategory.Price;
            return View(bodyShapingPacketDTO);
        }
        [HttpPost]
        public IActionResult UpdateComboPacket(int CategoryId, NewComboPacketCategoryDTO bodyShapingPacketDTO)
        {

            BodyShapingPacketCategory bodyShapingPacketCategory = _bodyShapingCategoryService.GetById(CategoryId);

            var validator = new AddComboPacketCategoryValidator();
            var validationResult = validator.Validate(bodyShapingPacketDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(bodyShapingPacketDTO);
            }
            bodyShapingPacketCategory.SessionCount = bodyShapingPacketDTO.SessionCount;
            bodyShapingPacketCategory.Packet = bodyShapingPacketDTO.PacketName;
            bodyShapingPacketCategory.SessionDuration = 0;
            bodyShapingPacketCategory.MainCategoryId = null;
            bodyShapingPacketCategory.Price = bodyShapingPacketDTO.Price;
            _bodyShapingCategoryService.Update(bodyShapingPacketCategory);
            return RedirectToAction("BodyShapingComboPacketList", "Categories");
        }
        #endregion
        #region Kosmetologiya Kateqoriyaları

        #endregion
        #region Lazer Kategoriyaları
        [HttpGet]
        public IActionResult LazerCategoryList()
        {

            List<LazerCategory> lazerCategories =_db.LazerCategories.Include(x=>x.MainCategory).Include(x=>x.ChildCategories).Include(x=>x.Filial).Where(x=>x.MainCategoryId!=null).ToList();
            return View(lazerCategories);
        }
        [HttpGet]
        public IActionResult LazerCategoryOFF(int CategoryId)
        {
            LazerCategory category = _lazerCategoryService.GetById(CategoryId);
            if (category.IsDeactive == false)
            {
                category.IsDeactive = true;
                _lazerCategoryService.Update(category);

                return RedirectToAction("LazerCategoryList", "Categories");
            }
            else
            {
                category.IsDeactive = false;
                _lazerCategoryService.Update(category);
                return RedirectToAction("LazerCategoryList", "Categories");
            }


        }
        [HttpGet]
        public IActionResult AddLazerCategory()
        {
           
            AddLazerCategoryDTO lazerCategoryDTO = new AddLazerCategoryDTO();
            lazerCategoryDTO.MainCategory = _db.LazerCategories.Include(x => x.MainCategory).Include(x => x.ChildCategories).Include(x => x.Filial).Where(x => x.MainCategoryId == null).ToList();
            lazerCategoryDTO.Filial=_db.Filials.Where(x=>x.Id!=4 && x.Id!=5).ToList();
            return View(lazerCategoryDTO);
        }
        [HttpPost]
        public IActionResult AddLazerCategory(AddLazerCategoryDTO lazerCategoryDTO)
        {
            lazerCategoryDTO.MainCategory = _db.LazerCategories.Include(x => x.MainCategory).Include(x => x.ChildCategories).Include(x => x.Filial).Where(x=>x.MainCategoryId==null).ToList();
            lazerCategoryDTO.Filial = _db.Filials.Where(x => x.Id != 4 && x.Id != 5).ToList();
            LazerCategory lazerCategory=new LazerCategory();
            var validator = new LazerCategoryValidator();
            var validationResult = validator.Validate(lazerCategoryDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(lazerCategoryDTO);
            }
            lazerCategory.MainCategoryId = lazerCategoryDTO.MainCategoryId;
            lazerCategory.FilialId=lazerCategoryDTO.FilialId;
            lazerCategory.Price=lazerCategoryDTO.Price; 
            lazerCategory.Name=lazerCategoryDTO.Name;
            lazerCategory.IsDeactive = false;
            _lazerCategoryService.Create(lazerCategory);
            return RedirectToAction("LazerCategoryList","Categories");
        }
        [HttpGet]
        public IActionResult UpdateLazerCategory(int CategoryId)
        {
            LazerCategory lazerCategory = _lazerCategoryService.GetById(CategoryId);

            AddLazerCategoryDTO lazerCategoryDTO = new AddLazerCategoryDTO();
            lazerCategoryDTO.MainCategory = _db.LazerCategories.Include(x => x.MainCategory).Include(x => x.ChildCategories).Include(x => x.Filial).Where(x => x.MainCategoryId == null).ToList();
            lazerCategoryDTO.Filial = _db.Filials.Where(x => x.Id != 4 && x.Id != 5).ToList();
            lazerCategoryDTO.Price = lazerCategory.Price;
            lazerCategoryDTO.FilialId = lazerCategory.FilialId;
            lazerCategoryDTO.MainCategoryId = (int)lazerCategory.MainCategoryId;
            lazerCategoryDTO.Name = lazerCategory.Name;

            return View(lazerCategoryDTO);
        }
        [HttpPost]
        public IActionResult UpdateLazerCategory(int CategoryId, AddLazerCategoryDTO lazerCategoryDTO)
        {
            lazerCategoryDTO.MainCategory = _db.LazerCategories.Include(x => x.MainCategory).Include(x => x.ChildCategories).Include(x => x.Filial).Where(x => x.MainCategoryId == null).ToList();
            lazerCategoryDTO.Filial = _db.Filials.Where(x => x.Id != 4 && x.Id != 5).ToList();
            LazerCategory lazerCategory = _lazerCategoryService.GetById(CategoryId);
            var validator = new LazerCategoryValidator();
            var validationResult = validator.Validate(lazerCategoryDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(lazerCategoryDTO);
            }
            lazerCategory.MainCategoryId = lazerCategoryDTO.MainCategoryId;
            lazerCategory.FilialId = lazerCategoryDTO.FilialId;
            lazerCategory.Price = lazerCategoryDTO.Price;
            lazerCategory.Name = lazerCategoryDTO.Name;
            lazerCategory.IsDeactive = false;
            _lazerCategoryService.Update(lazerCategory);
            return RedirectToAction("LazerCategoryList", "Categories");
        }
        #endregion
        #region Kosmetologiya Kategoriyalar
        [HttpGet]
        public IActionResult CosmetologyCategoryList()
        {
            List<CosmetologyCategory> cosmetologyCategories = _db.CosmetologyCategories.Include(x=>x.MainCategory).Include(x=>x.ChildCategory).Where(x => x.MainCategoryId != null).ToList();
            
            return View(cosmetologyCategories);
        }
        [HttpGet]
        public IActionResult CosmetologyMainCategoryList()
        {
            List<CosmetologyCategory> cosmetologyCategories = _db.CosmetologyCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory).Where(x => x.MainCategoryId == null).ToList();

            return View(cosmetologyCategories);
        }
        [HttpGet]
        public IActionResult AddMainCosmetologyCategory()
        {
            AddCosmetologyMainCategoryDTO addMainCosmetologyCategory = new AddCosmetologyMainCategoryDTO();
            return View(addMainCosmetologyCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMainCosmetologyCategory(AddCosmetologyMainCategoryDTO addMainCosmetologyCategory)
        {
            CosmetologyCategory cosmetologyCategory=new CosmetologyCategory();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ad boş ola bilməz!");
                return View(addMainCosmetologyCategory);
            }
            cosmetologyCategory.MainCategoryId = null;
            cosmetologyCategory.CategoryName=addMainCosmetologyCategory.CategoryName;
            cosmetologyCategory.IsDeactive = false;
            _cosmetologCategoryService.Create(cosmetologyCategory);
            return RedirectToAction("CosmetologyMainCategoryList", "Categories");
        }
        [HttpGet]
        public IActionResult AddCosmetologyCategory()
        {
            AddCosmetologyCategoryDTO addCosmetologyCategoryDTO= new AddCosmetologyCategoryDTO();
            addCosmetologyCategoryDTO.CosmetologyCategories = _db.CosmetologyCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory).Where(x => x.MainCategoryId == null).ToList();

            return View(addCosmetologyCategoryDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCosmetologyCategory(AddCosmetologyCategoryDTO addCosmetologyCategoryDTO)
        {
            addCosmetologyCategoryDTO.CosmetologyCategories=_db.CosmetologyCategories.Include(x=>x.MainCategory).Include(x=>x.ChildCategory).Where(x=>x.MainCategoryId==null).ToList();
            CosmetologyCategory cosmetologyCategory = new CosmetologyCategory();
            var validator = new CosmetologyCategoryValidator();
            var validationResult = validator.Validate(addCosmetologyCategoryDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(addCosmetologyCategoryDTO);
            }
            cosmetologyCategory.MainCategoryId = addCosmetologyCategoryDTO.MainCategoryId;
            cosmetologyCategory.CategoryName = addCosmetologyCategoryDTO.CategoryName;
            cosmetologyCategory.IsDeactive=false;

            _cosmetologCategoryService.Create(cosmetologyCategory); 
            return RedirectToAction("CosmetologyCategoryList", "Categories");
        }
        [HttpGet]
        public IActionResult UpdateMainCosmetologyCategory(int CategoryId)
        {
          CosmetologyCategory cosmetologyCategory=_cosmetologCategoryService.GetById(CategoryId);
            AddCosmetologyMainCategoryDTO cosmetologyMainCategoryDTO = new AddCosmetologyMainCategoryDTO();
            cosmetologyMainCategoryDTO.CategoryName = cosmetologyCategory.CategoryName;
            return View(cosmetologyMainCategoryDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateMainCosmetologyCategory(int CategoryId,AddCosmetologyMainCategoryDTO cosmetologyMainCategoryDTO)
        {
            CosmetologyCategory cosmetologyCategory = _cosmetologCategoryService.GetById(CategoryId);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ad boş ola bilməz!");
                return View(cosmetologyMainCategoryDTO);
            }
            cosmetologyCategory.CategoryName = cosmetologyMainCategoryDTO.CategoryName;
            _cosmetologCategoryService.Update(cosmetologyCategory);
            return RedirectToAction("CosmetologyMainCategoryList", "Categories");
        }
        [HttpGet]
        public IActionResult UpdateCosmetologyCategory(int CategoryId)
        {
            CosmetologyCategory cosmetologyCategory = _cosmetologCategoryService.GetById(CategoryId);
            AddCosmetologyCategoryDTO addCosmetologyCategoryDTO=new AddCosmetologyCategoryDTO();
            addCosmetologyCategoryDTO.CategoryName=cosmetologyCategory.CategoryName;
            addCosmetologyCategoryDTO.MainCategoryId =(int)cosmetologyCategory.MainCategoryId;
            addCosmetologyCategoryDTO.CosmetologyCategories = _db.CosmetologyCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory).Where(x => x.MainCategoryId == null).ToList();
            return View(addCosmetologyCategoryDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCosmetologyCategory(int CategoryId, AddCosmetologyCategoryDTO addCosmetologyCategoryDTO)
        {
            addCosmetologyCategoryDTO.CosmetologyCategories = _db.CosmetologyCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory).Where(x => x.MainCategoryId == null).ToList();
            CosmetologyCategory cosmetologyCategory = _cosmetologCategoryService.GetById(CategoryId);
            var validator = new CosmetologyCategoryValidator();
            var validationResult = validator.Validate(addCosmetologyCategoryDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(addCosmetologyCategoryDTO);
            }
            cosmetologyCategory.MainCategoryId = addCosmetologyCategoryDTO.MainCategoryId;
            cosmetologyCategory.CategoryName = addCosmetologyCategoryDTO.CategoryName;
            cosmetologyCategory.IsDeactive = false;

            _cosmetologCategoryService.Update(cosmetologyCategory);
            return RedirectToAction("CosmetologyCategoryList", "Categories");
          
        }
        [HttpGet]
        public IActionResult CosmetologyMainCategoryOFF(int CategoryId)
        {
            CosmetologyCategory category = _cosmetologCategoryService.GetById(CategoryId);
            if (category.IsDeactive == false)
            {
                category.IsDeactive = true;
                _cosmetologCategoryService.Update(category);

                return RedirectToAction("CosmetologyMainCategoryList", "Categories");
            }
            else
            {
                category.IsDeactive = false;
                _cosmetologCategoryService.Update(category);
                return RedirectToAction("CosmetologyMainCategoryList", "Categories");
            }


        }
        [HttpGet]
        public IActionResult CosmetologyCategoryOFF(int CategoryId)
        {
            CosmetologyCategory category = _cosmetologCategoryService.GetById(CategoryId);
            if (category.IsDeactive == false)
            {
                category.IsDeactive = true;
                _cosmetologCategoryService.Update(category);

                return RedirectToAction("CosmetologyCategoryList", "Categories");
            }
            else
            {
                category.IsDeactive = false;
                _cosmetologCategoryService.Update(category);
                return RedirectToAction("CosmetologyCategoryList", "Categories");
            }


        }
        #endregion
        #region Lipuçka Kategoriyaları
        [HttpGet]
        public IActionResult LipuckaCategoryList()
        {
            List<LipuckaCategories> lipuckaKategories=_db.LipuckaCategories.Include(x=>x.MainCategory).Where(x=>x.MainCategoryId!=null).ToList();
            return View(lipuckaKategories);
        }
        [HttpGet]
        public IActionResult AddLipuckaCategory()
        {
            AddLipuckaCategoryDTO LipuckaCategoryDTO = new AddLipuckaCategoryDTO();
            LipuckaCategoryDTO.MainCategories = _db.LipuckaCategories.Include(x => x.MainCategory).Where(x => x.MainCategoryId == null).ToList();
            
            return View(LipuckaCategoryDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLipuckaCategory(AddLipuckaCategoryDTO LipuckaCategoryDTO)
        {

            LipuckaCategoryDTO.MainCategories = _db.LipuckaCategories.Include(x => x.MainCategory).Where(x=>x.MainCategoryId==null).ToList();
            var validator = new LipuckaCategoryValidator();
            var validationResult = validator.Validate(LipuckaCategoryDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(LipuckaCategoryDTO);
            }
            LipuckaCategories lipuckaCategories=new LipuckaCategories();
            lipuckaCategories.MainCategoryId=LipuckaCategoryDTO.MainCategoryId;
            lipuckaCategories.Price = 0;
            lipuckaCategories.IsDeactive = false;
            lipuckaCategories.Name = LipuckaCategoryDTO.CategoryName;
            _lipuckaCategoriesService.Create(lipuckaCategories);
            return RedirectToAction("LipuckaCategoryList", "Categories");
        }
        [HttpGet]
        public IActionResult UpdateLipuckaCategory(int CategoryId)
        {
            AddLipuckaCategoryDTO LipuckaCategoryDTO = new AddLipuckaCategoryDTO();
            LipuckaCategories category = _lipuckaCategoriesService.GetById(CategoryId);
            LipuckaCategoryDTO.MainCategories = _db.LipuckaCategories.Include(x => x.MainCategory).Where(x => x.MainCategoryId == null).ToList();
            LipuckaCategoryDTO.CategoryName= category.Name;
            LipuckaCategoryDTO.MainCategoryId = (int)category.MainCategoryId;
            return View(LipuckaCategoryDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateLipuckaCategory(int CategoryId, AddLipuckaCategoryDTO LipuckaCategoryDTO)
        {

            LipuckaCategoryDTO.MainCategories = _db.LipuckaCategories.Include(x => x.MainCategory).Where(x => x.MainCategoryId == null).ToList();
            var validator = new LipuckaCategoryValidator();
            var validationResult = validator.Validate(LipuckaCategoryDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(LipuckaCategoryDTO);
            }
            LipuckaCategories category=_lipuckaCategoriesService.GetById(CategoryId);
            category.Name = LipuckaCategoryDTO.CategoryName;
            category.MainCategoryId = LipuckaCategoryDTO.MainCategoryId;
            _lipuckaCategoriesService.Update(category);
            return RedirectToAction("LipuckaCategoryList", "Categories");
        }
        [HttpGet]
        public IActionResult LipuckaCategoryOFF(int CategoryId)
        {
            LipuckaCategories category = _lipuckaCategoriesService.GetById(CategoryId);
            if (category.IsDeactive == true)
            {
                category.IsDeactive = false;
                _lipuckaCategoriesService.Update(category);
                return RedirectToAction("LipuckaCategoryList", "Categories");
            }
            else
            {
                category.IsDeactive = true;
                _lipuckaCategoriesService.Update(category);
                return RedirectToAction("LipuckaCategoryList", "Categories");
            }

        }
        #endregion
        #region Pirsinq Kateqoriyaları
        [HttpGet]
        public IActionResult PirsinqCategoryList()
        {
            List<PirsinqCategory> pirsinqCategories = _db.PirsinqCategories.Include(x => x.MainCategory).Where(x=>x.MainCategoryId!=null).ToList();
            return View(pirsinqCategories);
        }
        [HttpGet]
        public IActionResult AddPirsinqCategory()
        {
            AddPirsinqCategoryDTO dto = new AddPirsinqCategoryDTO();
            dto.MainCategories = _db.PirsinqCategories.Include(x => x.MainCategory).Where(x => x.MainCategoryId == null).ToList();

            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPirsinqCategory(AddPirsinqCategoryDTO dto)
        {
            dto.MainCategories = _db.PirsinqCategories.Include(x => x.MainCategory).Where(x => x.MainCategoryId == null).ToList();
            var validator = new PirsinqCategoryValidator();
            var validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(dto);
            }
            PirsinqCategory entity = new PirsinqCategory();
            entity.CategoryName = dto.CategoryName;
            entity.MainCategoryId=dto.MainCategoryId;
            entity.Price = 0;
            _pirsinqCategoriesService.Create(entity);
            return RedirectToAction("PirsinqCategoryList", "Categories");
        }
        [HttpGet]
        public IActionResult UpdatePirsinqCategory(int CategoryId)
        {
            AddPirsinqCategoryDTO dto = new AddPirsinqCategoryDTO();
            PirsinqCategory category = _pirsinqCategoriesService.GetById(CategoryId);
            dto.MainCategories = _db.PirsinqCategories.Include(x => x.MainCategory).Where(x => x.MainCategoryId == null).ToList();
            dto.CategoryName = category.CategoryName;
            dto.Price=category.Price;

            dto.MainCategoryId = (int)category.MainCategoryId;
         
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePirsinqCategory(int CategoryId, AddPirsinqCategoryDTO dto)
        {

            dto.MainCategories = _db.PirsinqCategories.Include(x => x.MainCategory).Where(x => x.MainCategoryId == null).ToList();
          
            PirsinqCategory category = _pirsinqCategoriesService.GetById(CategoryId);
      
            category.CategoryName = dto.CategoryName;
            category.Price = dto.Price;
            category.MainCategoryId = dto.MainCategoryId;
            _pirsinqCategoriesService.Update(category);
            return RedirectToAction("PirsinqCategoryList", "Categories");
        }
        [HttpGet]
        public IActionResult PirsinqCategoryOFF(int CategoryId)
        {
            PirsinqCategory category = _pirsinqCategoriesService.GetById(CategoryId);
            if (category.IsDeactive==true)
            {
                category.IsDeactive = false;
                _pirsinqCategoriesService.Update(category);
                return RedirectToAction("PirsinqCategoryList", "Categories");
            }
            else
            {
                category.IsDeactive=true;
                _pirsinqCategoriesService.Update(category);
                return RedirectToAction("PirsinqCategoryList", "Categories");
            }
           
        }
 

        #endregion
    }




}
