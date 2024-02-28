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
    public class PirsinqAppointmentManager : IPirsinqAppointmentService
    {
        private readonly IPirsinqAppointmentDAL _service;
        public PirsinqAppointmentManager(IPirsinqAppointmentDAL service)
        {

            _service = service;

        }
        public void Create(PirsinqAppointment t)
        {
            _service.Create(t);
        }

        public void Delete(PirsinqAppointment t)
        {
            _service.Delete(t);
        }

        public PirsinqAppointment GetById(int id)
        {
            return _service.GetById(id);    
        }

        public List<PirsinqAppointment> GetList()
        {
            return _service.GetList();
        }

        public void Update(PirsinqAppointment t)
        {
            _service.Update(t);
        }
    }
}
