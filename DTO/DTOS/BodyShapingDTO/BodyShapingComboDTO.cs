using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.BodyShapingDTO
{
    public class BodyShapingComboDTO
    {
        public string? PacketName { get; set; }

        public string Master { get; set; }

        public int BodyShapingMasterId { get; set; }

        public string CustomerName { get; set; }

        public decimal? Price { get; set; }

        public List<BodyShapingPacketCategory> BodyShapingPacketCategories { get; set; }

        public int CategoryId {  get; set; }   
        
        public string Description {  get; set; }    
        
    }
}
