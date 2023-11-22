using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Kassa
    {
       
        public int Id { get; set; }

        public decimal Budget { get; set; }

        public List<KassaActionList> KassaActionLists {get;set;} 

        public Filial Filial { get; set; }

        public int FilialId {  get; set; }
    }
}
