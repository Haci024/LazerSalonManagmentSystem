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
    public class SpendCategoryManager:ISpendCategoryService
    {
        private readonly ISpendCategoryDAL _DAL;    
        public SpendCategoryManager(ISpendCategoryDAL spendCategoryDAL)
        {
            _DAL=spendCategoryDAL;
        }

        public void Create(SpendCategory t)
        {
           _DAL.Create(t);
        }

        public void Delete(SpendCategory t)
        {
           _DAL.Delete(t);
        }

        public SpendCategory GetById(int id)
        {
            return _DAL.GetById(id);
        }

        public List<SpendCategory> GetList()
        {
           return _DAL.GetList();
        }

        public void Update(SpendCategory t)
        {
           _DAL.Update(t);
        }
    }
}
