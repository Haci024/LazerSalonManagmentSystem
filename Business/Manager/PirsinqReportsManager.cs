
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
    public class PirsinqReportsManager:IPirsinqReportsService
    {
        private readonly IPirsinqReportsDAL _pirsinqReportsDAL;
        public PirsinqReportsManager(IPirsinqReportsDAL pirsinqReportsDAL)
        {
            _pirsinqReportsDAL = pirsinqReportsDAL;
        }

        public void Create(PirsinqReports t)
        {
            _pirsinqReportsDAL.Create(t);
        }

        public void Delete(PirsinqReports t)
        {
            _pirsinqReportsDAL.Delete(t);
        }

        public PirsinqReports GetById(int id)
        {
            return _pirsinqReportsDAL.GetById(id);
        }

        public List<PirsinqReports> GetList()
        {
           return _pirsinqReportsDAL.GetList();
        }

        public void Update(PirsinqReports t)
        {
           _pirsinqReportsDAL.Update(t);
        }
    }
}
