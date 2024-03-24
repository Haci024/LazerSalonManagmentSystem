using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class SpendCategory
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public bool Status { get; set; }

        public Filial Filial { get; set;}  

        public int FilialId { get; set; }

        public bool AutoDate { get; set; }

        



    }
}
