using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class SolariumCategories
    {
        public int Id { get; set; }

        public SolariumCategories MainCategory { get; set; }

        public int? MainCategoryId { get; set; }

        public List<SolariumCategories> ChildCategories { get; set; }

        public string Name { get; set; }

        public int? Minute { get; set; }

        public decimal? Price { get; set; }

        public int? UsingPeriod { get; set; }

        public List<SolariumAppointment> SolariumAppointment { get; set;}

        





    }
}
