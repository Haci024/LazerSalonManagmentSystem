using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.BodyShapingDTO
{
    public class SessionListDTO
    {
        public bool IsComboPacket {  get; set; }

        public List<BodyShapingSessionList> SessionLists {  get; set; }  
    }
}
