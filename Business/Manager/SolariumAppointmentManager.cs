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
    
    public class SolariumAppointmentManager : ISolariumAppointmentService
    {
        private readonly ISolariumAppointmentDAL _service;
        public SolariumAppointmentManager(ISolariumAppointmentDAL service)
        {

            _service = service;

        }
        public void Create(SolariumAppointment t)
        {
            _service.Create(t);
        }

        public void Delete(SolariumAppointment t)
        {
            _service.Delete(t);
        }

      

        public SolariumAppointment GetById(int id)
        {
           return _service.GetById(id); 
        }

        public List<SolariumAppointment> GetDailySolariumAppointmentReports()
        {
            return _service.DailySolariumReportList();
        }

        public List<SolariumAppointment> GetList()
        {
            return _service.GetList();
        }

        public void Update(SolariumAppointment t)
        {
          _service.Update(t);
        }
    }
}
