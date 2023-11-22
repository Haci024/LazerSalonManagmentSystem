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
        public List<LazerAppointment> GetAllSuccecfullyAppointments();
        public List<LazerAppointment> GetAllInjectList();
        public List<LazerAppointment> DailyLazerAppointmentReports();
        public Task<LazerAppointment> SelectLazerAppointment(int AppointmentId);
        public List<LazerAppointment> AllReservations(AppUser appUser);
        public List<LazerAppointment> NextSessionList(AppUser appUser);
        public List<LazerAppointment> InComepletedList(AppUser appUser);
        public  Task<LazerAppointment> CompletelySecondSessionEnd(int AppointmentId);
        public  Task<LazerAppointment> CompletetedSecondSessionStart(int AppointmentId);

    }
}
