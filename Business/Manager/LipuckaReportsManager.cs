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
    public class LipuckaReportsManager:ILipuckaReportsService
    {
        private readonly ILipuckaReportsDAL _dal;
        public LipuckaReportsManager(ILipuckaReportsDAL dAL)
        {
            _dal = dAL;
        }

        public void Create(LipuckaReports t)
        {
            _dal.Create(t);
        }

        public void Delete(LipuckaReports t)
        {
            _dal.Delete(t); 
        }

        public LipuckaReports GetById(int id)
        {
            return _dal.GetById(id);
        }

        public List<LipuckaReports> GetList()
        {
            return _dal.GetList();  
        }

        public void Update(LipuckaReports t)
        {
           _dal.Update(t);
        }
    }
}
