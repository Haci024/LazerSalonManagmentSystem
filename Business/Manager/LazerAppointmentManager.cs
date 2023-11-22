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

    public class LazerAppointmentManager :ILazerAppointmentService
    {
        private readonly ILazerAppointmentDAL _appointmentDAL;
        public LazerAppointmentManager(ILazerAppointmentDAL appointmentDAL)
        {
            _appointmentDAL = appointmentDAL;
        }
        public void Create(LazerAppointment t)
        {
            _appointmentDAL.Create(t);
        }

        public void Delete(LazerAppointment t)
        {
             _appointmentDAL.Delete(t);
        }

        public LazerAppointment GetById(int id)
        {
            return _appointmentDAL.GetById(id); 
        }

        public List<LazerAppointment> GetList()
        {
            return _appointmentDAL.GetList();
        }

        public void Update(LazerAppointment t)
        {
            _appointmentDAL.Update(t);
        }
    

        public List<LazerAppointment> GetAllInjectList()
        {
            return _appointmentDAL.GetAllInjectList();
        }

        public List<LazerAppointment> GetAllSuccecfullyAppointments()
        {
            return _appointmentDAL.SuccesfullyAppointments();
        }

        public List<LazerAppointment> DailyLazerAppointmentReports()
        {
            return _appointmentDAL.LazerAppointmentsReports();
        }

        public Task<LazerAppointment> SelectLazerAppointment(int AppointmentId)
        {
            return _appointmentDAL.SelectedCustomer(AppointmentId);
        }

        public List<LazerAppointment> AllReservations(AppUser appUser)
        {
           return _appointmentDAL.AllReservations(appUser);
        }

        public List<LazerAppointment> NextSessionList(AppUser appUser)
        {
            return _appointmentDAL.NextSessionList(appUser);
        }

        public List<LazerAppointment> InComepletedList(AppUser appUser)
        {
            return _appointmentDAL.InCompletedList(appUser);
        }
        public async Task<LazerAppointment> CompletetedSecondSessionStart(int AppointmentId)
        {
            return await _appointmentDAL.CompletetedSecondSessionStart(AppointmentId);
        }
        public async Task<LazerAppointment> CompletelySecondSessionEnd(int AppointmentId)
        {
            return await _appointmentDAL.CompletelySecondSessionEnd(AppointmentId);
        }

      
    }
}
