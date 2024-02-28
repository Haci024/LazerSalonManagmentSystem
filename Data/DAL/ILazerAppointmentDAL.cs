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
        public Task<List<LazerAppointment>> GetAllInjectList();

        public Task<List<LazerAppointment>> SuccesfullyAppointments(int filialId);

        public Task<List<LazerAppointment>> LazerAppointmentsReports();
       
        public Task<List<LazerAppointment>> ReservationsForMaster(int filialId,int LazerMasterId);

        public Task<List<LazerAppointment>> AllReservations(int filialId);


        public Task<LazerAppointment> SelectedCustomer(int AppointmentId);

        public Task<List<LazerAppointment>> NextSessionList(int filialId);

        public  Task<List<LazerAppointment>> InCompletedList(int filialId);

        public Task<LazerAppointment> CompletetedSecondSessionStart(int AppointmentId);

        public  Task<LazerAppointment> CompletelySecondSessionEnd(int AppointmentId);

        public Task<List<LazerAppointment>> InjectionsForMaster(int filialId,int lazermasterId);

    }
}
