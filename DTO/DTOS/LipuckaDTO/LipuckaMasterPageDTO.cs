using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LipuckaDTO
{
    public  class LipuckaMasterPageDTO
    {
        public  List<LipuckaAppointment> InjectionList;

        public List<LipuckaAppointment> ReservationList;

        public List<Customer> Customers;

        public int LipuckaMasterId;


    }
}
