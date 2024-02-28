using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class OutMoney
    {
        public int Id { get; set;}

        public DateTime AddingDate { get; set;}

        public Decimal Price { get; set; }

        public string Description {get; set;}

        public AppUser AppUser { get; set;}

        public string AppUserId { get; set;}

        public  SpendCategory SpendCategory { get; set;}

        public int SpendCategoryId { get; set;}
    }
}
