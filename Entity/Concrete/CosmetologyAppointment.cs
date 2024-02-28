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
   
        public Cosmetologs Cosmetolog { get; set;}

        public int CosmetologId { get; set; }

        public int CustomerId { get; set; }

        public Customer Customers { get; set;}

        public decimal Price { get; set;}

        public DateTime? StartTime { get; set; }

        public DateTime? OutTime { get; set; }

        public AppUser AppUser { get; set; }

        public string AppUserId { get; set;}

        public string? CosmetologyDescription { get; set;}

        public bool IsStart { get;set; }

        public bool IsCompleted { get;set; }

        public List<CosmetologyReport> CosmetologyReports { get; set;}

        public Filial Filial { get; set;}   

        public int FilialId { get; set; }

        

    }
}
