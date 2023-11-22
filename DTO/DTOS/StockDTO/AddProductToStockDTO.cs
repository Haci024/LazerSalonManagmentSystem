using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.StockDTO
{
    public class AddProductToStockDTO
    {
        public int ProductCount { get;set; }

        public string ProductName { get;set; }  

        public decimal BuyingPrice { get;set; }

        public decimal SellingPrice { get; set; }
    }
}
