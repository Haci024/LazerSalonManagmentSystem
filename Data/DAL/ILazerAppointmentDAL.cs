using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public interface ILazerAppointmentDAL:IGenericDAL<LazerAppointment>
    {
        public List<LazerAppointment> GetAllInjectList();

        public List<LazerAppointment> SuccesfullyAppointments();

        public List<LazerAppointment> LazerAppointmentsReports();
       
        public List<LazerAppointment> AllReservations(AppUser appUser);


        public Task<LazerAppointment> SelectedCustomer(int AppointmentId);

        public List<LazerAppointment> NextSessionList(AppUser appUser);

        public List<LazerAppointment> InCompletedList(AppUser appUser);

        public Task<LazerAppointment> CompletetedSecondSessionStart(int AppointmentId);

        public  Task<LazerAppointment> CompletelySecondSessionEnd(int AppointmentId);

    }
}
