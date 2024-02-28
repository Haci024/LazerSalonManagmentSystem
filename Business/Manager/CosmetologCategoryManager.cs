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
    public class CosmetologCategoryManager : ICosmetologCategoryService
    {
        private readonly ICosmetologCategoryDAL _dal;
        public CosmetologCategoryManager(ICosmetologCategoryDAL cosmetologCategoryDAL)
        {
            _dal = cosmetologCategoryDAL;
        }
        public void Create(CosmetologyCategory t)
        {
            _dal.Create(t);
        }

        public void Delete(CosmetologyCategory t)
        {
            _dal.Delete(t);
        }

        public CosmetologyCategory GetById(int id)
        {
            return _dal.GetById(id);    
        }

        public List<CosmetologyCategory> GetList()
        {
            return _dal.GetList();
        }

        public void Update(CosmetologyCategory t)
        {
            _dal.Update(t);
        }
    }
}
