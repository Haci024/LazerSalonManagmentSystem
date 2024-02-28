using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class SolariumAppointment
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public  DateTime BuyingDate { get; set; }

        public decimal Price { get; set; }

        public int RemainingMinute { get; set; }

        public int MinuteLimit { get; set; }

        public int UsingMinute { get; set; } = 0;

        public SolariumCategories SolariumCategories { get; set; }
      
        public int SolariumCategoriesId { get; set; }

        public List<SolariumUsingList> SolariumUsingList { get; set; }

        public Filial Filial { get; set; }

        public int FilialId { get; set; }

        public AppUser AppUser { get; set; } 

        public string AppUserId { get; set;}

        public DateTime RemainingTime { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        public bool IsTimeOut { get; set; } = false;

        public bool IsDeleted { get; set; }=false;

        public decimal ReturnMoney {  get; set; }=decimal.Zero;

    }
}
