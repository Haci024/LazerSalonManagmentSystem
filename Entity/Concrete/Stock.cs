using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Stock
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int ProductCount { get; set; }   

        public DateTime AddingDate { get; set; }

        public List<Income> Incomes { get; set; }

        public AppUser AppUser { get; set; }    

        public string AppUserId { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal BuyingPrice { get; set; }

        public Filial Filial { get; set; }

        public int FilialId { get; set; }

    }
}
