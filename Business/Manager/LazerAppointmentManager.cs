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
    

        public async  Task<List<LazerAppointment>> GetAllInjectList()
        {
            return await _appointmentDAL.GetAllInjectList();
        }

        public async Task<List<LazerAppointment>> GetAllSuccecfullyAppointments(int filialId)
        {
            return await _appointmentDAL.SuccesfullyAppointments(filialId);
        }
        
        public async Task<List<LazerAppointment>> DailyLazerAppointmentReports()
        {
            return await _appointmentDAL.LazerAppointmentsReports();
        }

        public async Task<LazerAppointment> SelectLazerAppointment(int AppointmentId)
        {
            return await _appointmentDAL.SelectedCustomer(AppointmentId);
        }

        public async Task<List<LazerAppointment>> ReservationsForMaster(int filialId, int LazerMasterId)
        {
           return await _appointmentDAL.ReservationsForMaster(filialId,LazerMasterId);
        }

        public async Task<List<LazerAppointment>> NextSessionList(int filialId)
        {
            return await _appointmentDAL.NextSessionList(filialId);
        }

        public async Task<List<LazerAppointment>> InComepletedList(int filialId)
        {
            return await _appointmentDAL.InCompletedList(filialId);
        }
        public async Task<LazerAppointment> CompletetedSecondSessionStart(int AppointmentId)
        {
            return await _appointmentDAL.CompletetedSecondSessionStart(AppointmentId);
        }
        public async Task<LazerAppointment> CompletelySecondSessionEnd(int AppointmentId)
        {
            return await _appointmentDAL.CompletelySecondSessionEnd(AppointmentId);
        }

        public async Task<List<LazerAppointment>> AllReservations(int filialId)
        {
            return await _appointmentDAL.AllReservations(filialId);
        }

        public async Task<List<LazerAppointment>> InjectionsForMaster(int filialId, int lazermasterId)
        {
            return await _appointmentDAL.InjectionsForMaster(filialId, lazermasterId);
        }
    }
}
