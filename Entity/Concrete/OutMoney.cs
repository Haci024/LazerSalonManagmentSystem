using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class OutMoney
    {
        public int Id { get; set; }

        public DateTime CreateTime { get; set; }

        public string Product { get; set; }

        public Decimal Price { get; set; }

        public string Description { get; set; }

        public AppUser AppUser { get; set; }

        public string AppUserId { get; set; }

        public Filial Filial { get; set; }

        public int? FilialId { get; set; }
    }
}
