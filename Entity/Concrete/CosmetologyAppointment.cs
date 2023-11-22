using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class CosmetologyAppointment
    {
        public int Id { get; set;}

        public int CosmetologId { get; set;}

        public Cosmetologs Cosmetolog { get; set;}

        public int CustomerId { get; set; }

        public Customer Customers { get; set;}

        public decimal Price { get; set;}

        public DateTime ReservationDate { get; set;}

        public DateTime StartTime { get; set; }

        public DateTime OutTime { get; set; }

    }
}
