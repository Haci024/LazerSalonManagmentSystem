using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.StockDTO
{
    public class FilialDepoDTO
    {
        public List<Stock> ArzumMiniStock { get; set; }

        public List<Stock> ArzumBeautyStock { get;  set; }

        public List<Stock> ArzumEsteticStock { get; set; }


    }
}
