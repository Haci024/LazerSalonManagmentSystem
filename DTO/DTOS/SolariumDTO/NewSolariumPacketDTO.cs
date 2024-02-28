using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.SolariumDTO
{
    public class NewSolariumPacketDTO
    {
       public int MainCategoryId { get; set; }

        public string CategoryName { get; set; }

        public List<SolariumCategories> MainCategories { get; set; }

        public int Minute { get; set; }

        public decimal Price { get; set; }

        public int UsingPeriod { get; set; }


    }
}
