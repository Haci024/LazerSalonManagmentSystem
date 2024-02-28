using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LazerAppointmentDTO
{
    public class LazerMasterPageDTO
    {
        public List<LazerAppointment> LazerAppointments { get; set; }

        public List<LazerAppointment> Injections { get; set; }

        public List<Customer> CustomerList { get; set; }

        public int  LazerMasterId { get; set; }

        
    }
}
