using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.CosmetologyDTO
{
    public class AddNewSessionDTO
    {
        public string CosmetologName  { get; set; }

        public string CustomerName { get; set; }

        public int CosmetologId { get; set; }

        public List<CosmetologyCategory> CosmetologyCategories { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

       

        public int[] CategoriesId { get;set; }

      
    }
}
