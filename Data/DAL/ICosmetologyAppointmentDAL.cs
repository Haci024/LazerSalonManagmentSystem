using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public interface ICosmetologyAppointmentDAL:IGenericDAL<CosmetologyAppointment>
    {
        public  Task<List<CosmetologyAppointment>> CosmetologReservation();

        public  Task<CosmetologyAppointment> SelectedAppointment(int appointmentId);
    }
}
