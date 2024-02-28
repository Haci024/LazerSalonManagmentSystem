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
    public class CosmetologyReportManager : ICosmetologyReportService
    {
        private readonly ICosmetologyReportDAL _dal;    
        public CosmetologyReportManager(ICosmetologyReportDAL dal)
        {
            _dal = dal;
        }
        public void Create(CosmetologyReport t)
        {
            _dal.Create(t);
        }

        public void Delete(CosmetologyReport t)
        {
            _dal.Delete(t);
        }

        public CosmetologyReport GetById(int id)
        {
            return _dal.GetById(id);
        }

        public List<CosmetologyReport> GetList()
        {
            return _dal.GetList();
        }

        public void Update(CosmetologyReport t)
        {
            _dal.Update(t);
        }
    }
}
