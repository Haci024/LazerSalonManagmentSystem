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
    public class OutMoneyManager : IOutMoneyService
    {
        private readonly IOutMoneyDAL _IOutMoneyDAL;
        public OutMoneyManager(IOutMoneyDAL outMoneyDAL)
        {
           _IOutMoneyDAL = outMoneyDAL;
        }
        public void Create(OutMoney t)
        {
            _IOutMoneyDAL.Create(t);
        }

        public void Delete(OutMoney t)
        {
            _IOutMoneyDAL.Delete(t);
        }

        public OutMoney GetById(int id)
        {
           return _IOutMoneyDAL.GetById(id);
        }

        public List<OutMoney> GetList()
        {
            return _IOutMoneyDAL.GetList();
        }

        public void Update(OutMoney t)
        {
            _IOutMoneyDAL.Update(t);
        }
    }
}
