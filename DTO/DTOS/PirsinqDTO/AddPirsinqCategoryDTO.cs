using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.PirsinqDTO
{
    public class AddPirsinqCategoryDTO
    {
        public string CategoryName { get; set; }

        public int MainCategoryId { get; set; }

        public Decimal Price { get; set; }  

        public List<PirsinqCategory> MainCategories { get; set; }
    }
}
