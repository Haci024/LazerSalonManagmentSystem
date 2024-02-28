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
    public class CosmetologAppointmentManager:ICosmetologyAppointmentService
    {
        private readonly ICosmetologyAppointmentDAL _dal;
        public CosmetologAppointmentManager(ICosmetologyAppointmentDAL dal)
        {
            _dal = dal;
        }

        public Task<List<CosmetologyAppointment>> CosmetologReservation()
        {
            return _dal.CosmetologReservation();
        }

        public void Create(CosmetologyAppointment t)
        {
            _dal.Create(t);
        }

        public void Delete(CosmetologyAppointment t)
        {
            _dal.Delete(t);
        }

        public CosmetologyAppointment GetById(int id)
        {
            return _dal.GetById(id);
        }

        public List<CosmetologyAppointment> GetList()
        {
            return _dal.GetList();
        }

        public void Update(CosmetologyAppointment t)
        {
           _dal.Update(t);
        }

    }
}
