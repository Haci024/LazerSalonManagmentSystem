using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface ILazerAppointmentService:IGenericService<LazerAppointment>
    {
       
        public Task<List<LazerAppointment>> GetAllInjectList();
        public Task<List<LazerAppointment>> DailyLazerAppointmentReports();
        public Task<LazerAppointment> SelectLazerAppointment(int AppointmentId);
        public Task<List<LazerAppointment>> ReservationsForMaster(int filialId,int LazerMasterId);
        public Task<List<LazerAppointment>> AllReservations(int filialId);
        public Task<List<LazerAppointment>> NextSessionList(int filialId);
        public Task<List<LazerAppointment>> InComepletedList(int filialId);
        public  Task<LazerAppointment> CompletelySecondSessionEnd(int AppointmentId);
        public  Task<LazerAppointment> CompletetedSecondSessionStart(int AppointmentId);
        public Task<List<LazerAppointment>> InjectionsForMaster(int filialId,int lazermasterId);

    }
}
