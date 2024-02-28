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
    public class SolariumCategoryManager : ISolariumCategoryService
    {
        private readonly ISolariumCategoryDAL _categoryDAL;
        public SolariumCategoryManager(ISolariumCategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }
        public void Create(SolariumCategories t)
        {
            _categoryDAL.Create(t);
        }

        public void Delete(SolariumCategories t)
        {
            _categoryDAL.Delete(t);
        }

        public SolariumCategories GetById(int id)
        {
            return _categoryDAL.GetById(id);
        }

        public List<SolariumCategories> GetList()
        {
            return _categoryDAL.GetList();
        }

        public void Update(SolariumCategories t)
        {
            _categoryDAL.Update(t);
        }
    }
}
