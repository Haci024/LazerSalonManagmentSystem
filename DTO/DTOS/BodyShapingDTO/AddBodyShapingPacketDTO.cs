using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.BodyShapingDTO
{
    public class AddBodyShapingPacketDTO
    {
        public string Packet { get; set; }

        public List<BodyShapingPacketCategory> MainCategory { get; set; }

        public int MainCategoryId { get; set; }

        public int SessionCount { get; set; }

        public int SessionDuration { get; set; }

        public decimal Price { get; set; }

      
    }
}
