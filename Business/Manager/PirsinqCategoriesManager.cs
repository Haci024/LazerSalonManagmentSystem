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
    public class PirsinqCategoriesManager : IPirsinqCategoriesService
    {
        private readonly IPirsinqCategoriesDAL _dal;
        public PirsinqCategoriesManager(IPirsinqCategoriesDAL pirsinqCategoriesDAL)
        {
            _dal = pirsinqCategoriesDAL;
        }
        public void Create(PirsinqCategory t)
        {
            _dal.Create(t);
        }

        public void Delete(PirsinqCategory t)
        {
           _dal.Delete(t);
        }

        public PirsinqCategory GetById(int id)
        {
           return _dal.GetById(id);
        }

        public  Task<List<PirsinqCategory>> GetFemaleCategoryList()
        {
            return  _dal.GetFemaleCategories();
        }

        public List<PirsinqCategory> GetList()
        {
            return _dal.GetList();
        }

        public Task<List<PirsinqCategory>> GetMaleCategoryList()
        {
            return _dal.GetMaleCategories();
        }

        public void Update(PirsinqCategory t)
        {
            _dal.Update(t);
        }
    }
}
