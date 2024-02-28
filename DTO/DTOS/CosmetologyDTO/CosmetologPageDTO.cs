using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.CosmetologyDTO
{
    public class CosmetologPageDTO
    {
        public List<CosmetologyAppointment> CosmetologyAppointments { get; set; }   
        public List<CosmetologyAppointment> InJectionAppointments { get; set; }
        public List<Customer> Customers { get; set; }
       public int CosmetologId { get; set; }
    }
}
