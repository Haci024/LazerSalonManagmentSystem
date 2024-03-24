using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface ICosmetologyAppointmentService:IGenericService<CosmetologyAppointment>
    {
        public Task<List<CosmetologyAppointment>> CosmetologReservation();

        public Task<CosmetologyAppointment> SelectedAppointment(int AppointmentId);
    }
}
