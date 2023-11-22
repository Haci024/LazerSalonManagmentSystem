using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class KassaActionList
    {
        public int Id { get;set; }

        public DateTime LastOutMoneyDate {get; set;}

        public AppUser AppUser {get; set;}

        public Kassa Kassa { get; set;}

        public int KassaId { get; set;}

        public string AppUserId { get; set;}

        public decimal OutMoneyQuantity { get; set;}
    }
}
