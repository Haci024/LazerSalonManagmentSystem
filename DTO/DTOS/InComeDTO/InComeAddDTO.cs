using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.InComeDTO
{
    public class InComeAddDTO
    {

        public int Count { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public List<Stock> Stocks { get; set; }

        public int StockId { get; set; }


    }
}
