using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class CosmetologFilial
    {
        public int Id { get;set; }

        public Cosmetologs Cosmetologs { get; set; }

        public int CosmetologsId { get; set; }

        public Filial Filials { get; set; }

        public int FilialId { get; set; }
    }
}
