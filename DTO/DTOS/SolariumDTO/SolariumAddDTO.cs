using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.SolariumDTO
{
    public class SolariumAddDTO
    {

        public string MainCategory { get; set; }
      
       
        public int ChildCategoryId { get; set; }

        public string CustomerName { get; set; }

        public decimal Price { get; set; }

        public int MinuteLimit { get; set; }

        public List<SolariumCategories> SolariumCategories { get; set; }



    }
}
