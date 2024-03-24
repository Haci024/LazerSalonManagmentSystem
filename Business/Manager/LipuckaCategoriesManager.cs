using Business.IServices;
using Data.DAL;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager
{
    public class LipuckaCategoriesManager : ILipuckaCategoriesService
    {
        private readonly ILipuckaCategoriesDAL _dal;
        public LipuckaCategoriesManager(ILipuckaCategoriesDAL dAL)
        {
            _dal = dAL;   
        }

        public void Create(LipuckaCategories t)
        {
            _dal.Create(t);
        }

        public void Delete(LipuckaCategories t)
        {
            _dal.Delete(t);
        }

        public LipuckaCategories GetById(int id)
        {
            return _dal.GetById(id);    
        }

        public Task<List<LipuckaCategories>> GetFemaleCategoryList()
        {
            return _dal.GetFemaleCategoryList();
        }

        public List<LipuckaCategories> GetList()
        {
            return _dal.GetList();
        }

        public Task<List<LipuckaCategories>> GetMaleCategoryList()
        {
            return _dal.GetMaleCategoryList();
        }

        public void Update(LipuckaCategories t)
        {
            _dal.Update(t);
        }
        
    }
}
