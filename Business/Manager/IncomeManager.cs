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
    public class IncomeManager : InComeService
    {
        private readonly InComeDAL _inComeDAL;
        public IncomeManager(InComeDAL inComeDAL)
        {
            _inComeDAL = inComeDAL;
        }
        public void Create(Income t)
        {
            _inComeDAL.Create(t);
        }

        public void Delete(Income t)
        {
            _inComeDAL.Delete(t);
        }

        public Income GetById(int id)
        {
           return _inComeDAL.GetById(id);
        }

        public List<Income> GetList()
        {
            return _inComeDAL.GetList();
        }

        public void Update(Income t)
        {
             _inComeDAL.Update(t);
        }
    }
}
