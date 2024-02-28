using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface ISolariumAppointmentService:IGenericService<SolariumAppointment>
    {
        public List<SolariumAppointment> GetDailySolariumAppointmentReports();
       
    }
}
