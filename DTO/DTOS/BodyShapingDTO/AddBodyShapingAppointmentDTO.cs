using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.BodyShapingDTO
{
    public class AddBodyShapingAppointmentDTO
    {

        public int[] ChildCategoryId { get; set; }


        public List<BodyShapingPacketCategory> ChildCategories { get; set; }

        public decimal Price { get; set; }

        public string BodyShapingMaster { get; set; }

        public string FullName { get; set; }

        public int BodyShapingMasterId { get; set; }

        public string Description { get; set; } 



    }
}
