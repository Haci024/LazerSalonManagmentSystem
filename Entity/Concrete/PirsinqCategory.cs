using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class PirsinqCategory
    {
        public int Id { get; set; } 

        public string CategoryName { get; set; }

         public PirsinqCategory MainCategory { get; set; }

        public int? MainCategoryId { get; set; }

        public List<PirsinqCategory> ChildCategory { get; set; }

        public decimal Price {  get; set; } 

        public bool IsDeactive {  get; set; }

        public List<PirsinqReports> PirsinqReports { get; set; }

    }
}
