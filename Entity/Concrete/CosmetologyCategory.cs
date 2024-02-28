using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class CosmetologyCategory
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public CosmetologyCategory MainCategory { get; set; } 

        public List<CosmetologyCategory> ChildCategory { get; set; }

        public int? MainCategoryId { get; set;} 

        public List<CosmetologyReport> CosmetologyReports { get; set; }   

        public bool IsDeactive { get;set; }
    }
}
