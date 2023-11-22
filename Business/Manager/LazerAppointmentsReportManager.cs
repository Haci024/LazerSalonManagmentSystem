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
    public class LazerAppointmentsReportManager : ILazerAppointmentsReportService
    {
        private readonly ILazerAppointmentReportsDAL _dal;
        public LazerAppointmentsReportManager(ILazerAppointmentReportsDAL dal)
        {
            _dal = dal;
        }

        public void Create(LazerAppointmentReports t)
        {
           _dal.Create(t);  
        }

        public void Delete(LazerAppointmentReports t)
        {
            _dal.Delete(t); 
        }

        public LazerAppointmentReports GetById(int id)
        {
            return _dal.GetById(id);
        }

        public List<LazerAppointmentReports> GetList()
        {
            return _dal.GetList();
        }

        public void Update(LazerAppointmentReports t)
        {
            _dal.Update(t);
        }
    }
}
