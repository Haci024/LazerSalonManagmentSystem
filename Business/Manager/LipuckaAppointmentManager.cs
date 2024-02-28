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
    public class LipuckaAppointmentManager:ILipuckaAppointmentService
    {
        private readonly ILipuckaAppointmentDAL _dal;
        public LipuckaAppointmentManager(ILipuckaAppointmentDAL dal)
        {
            _dal = dal;
        }

        public void Create(LipuckaAppointment t)
        {
            _dal.Create(t);
        }

        public void Delete(LipuckaAppointment t)
        {
            _dal.Delete(t);
        }

        public LipuckaAppointment GetById(int id)
        {
           return _dal.GetById(id); 
        }

        public List<LipuckaAppointment> GetList()
        {
            return _dal.GetList();
        }

        public void Update(LipuckaAppointment t)
        {
            _dal.Update(t);
        }
    }
}
