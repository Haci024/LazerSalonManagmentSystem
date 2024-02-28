using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.CosmetologyDTO
{
    public class AddCosmetologyCategoryDTO
    {
        public List<CosmetologyCategory> CosmetologyCategories { get; set; }

        public int MainCategoryId {  get; set; }

        public string CategoryName { get; set; }

       
    }
}
