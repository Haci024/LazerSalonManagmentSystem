using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class LazerMasterFilial
    {
        public int Id { get; set; }

        public LazerMaster LazerMaster { get; set; }

        public int LazerMasterId { get; set; }

        public Filial Filial { get; set; }

        public int FilialId { get; set; }

    }
}
