using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.PirsinqDTO
{
    public class PirsinqMasterPageDTO
    {
        public List<PirsinqAppointment> InjectionList;

        public List<PirsinqAppointment> ReservationList;

        public List<Customer> Customers;

        public int PirsinqMasterId;
    }
}
