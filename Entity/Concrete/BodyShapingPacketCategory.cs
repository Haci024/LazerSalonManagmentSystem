using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class BodyShapingPacketCategory
    {
        public int Id { get; set; }

        public string Packet { get; set; }

        public BodyShapingPacketCategory MainCategory { get; set; }

        public List<BodyShapingPacketCategory> ChildCategory { get; set; }

        public int? MainCategoryId { get; set; }

        public int? SessionCount { get; set; }

        public int? SessionDuration { get; set; }

        public List<BodyShapingPacketsReports> BodyShapingPacketsReports { get; set; }

        public decimal? Price { get; set; }

        public bool IsDeactive { get; set; }


    }
}
