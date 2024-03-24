using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LipuckaDTO
{
    public class AddLipuckaCategoryDTO
    {
        public string CategoryName { get; set; }

        public int MainCategoryId { get; set; }

        public List<LipuckaCategories> MainCategories { get; set; }
    }
}
