using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface ILazerMasterService:IGenericService<LazerMaster>
    {
        public Task<List<LazerMaster>> GetLazeroloqListByFilial(int filialId);
        public Task<List<LazerMaster>> AllLazeroloq();
    }
}
