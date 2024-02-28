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
    public class BodyShapingAppointmentManager : IBodyShapingAppointmentService
    {
        private readonly IBodyShapingAppointmentDAL _bodyShapingAppointmentDAL;
        public BodyShapingAppointmentManager(IBodyShapingAppointmentDAL bodyShapingAppointmentDAL)
        {
            _bodyShapingAppointmentDAL = bodyShapingAppointmentDAL;
        }

        public void Create(BodyshapingAppointment t)
        {
            _bodyShapingAppointmentDAL.Create(t);
        }

        public void Delete(BodyshapingAppointment t)
        {
            _bodyShapingAppointmentDAL.Delete(t);
        }

        public BodyshapingAppointment GetById(int id)
        {

            return _bodyShapingAppointmentDAL.GetById(id);
        }

        public List<BodyshapingAppointment> GetList()
        {
            return _bodyShapingAppointmentDAL.GetList();
        }

        public void Update(BodyshapingAppointment t)
        {
            _bodyShapingAppointmentDAL.Update(t);
        }
    }
}
