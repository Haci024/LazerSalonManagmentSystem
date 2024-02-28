using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class LipuckaCategories
    {
       public int Id { get; set; }  

        public string Name { get; set; }

        public LipuckaCategories MainCategory { get; set; }

        public List<LipuckaCategories> ChildCategories { get; set; }  

        public int? MainCategoryId { get; set; }

        public decimal Price { get; set; }

        public bool IsDeactive { get; set; }=false;
    }
}
