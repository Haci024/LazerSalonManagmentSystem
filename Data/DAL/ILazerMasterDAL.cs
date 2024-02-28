using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public interface ILazerMasterDAL:IGenericDAL<LazerMaster>
    {
        public  Task<List<LazerMaster>> GetLazeroloqListByFilial(int filialId);
        public Task<List<LazerMaster>> AllLazeroloq();
    }
    
}
