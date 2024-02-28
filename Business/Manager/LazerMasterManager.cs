using Business.IServices;
using Data.DAL;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager
{
    public class LazerMasterManager : ILazerMasterService
    {
        private readonly ILazerMasterDAL _lazerMasterDAL;
        public LazerMasterManager(ILazerMasterDAL dal)
        {
            _lazerMasterDAL = dal;
        }
        public void Create(LazerMaster t)
        {
            _lazerMasterDAL.Create(t);
        }

        public void Delete(LazerMaster t)
        {
            _lazerMasterDAL.Delete(t);
        }

        public LazerMaster GetById(int id)
        {
           return _lazerMasterDAL.GetById(id);
        }

        public List<LazerMaster> GetList()
        {
            return _lazerMasterDAL.GetList();
        }

        public void Update(LazerMaster t)
        {
            _lazerMasterDAL.Update(t);
        }
        public async Task<List<LazerMaster>> GetLazeroloqListByFilial(int filialId) {

            return await _lazerMasterDAL.GetLazeroloqListByFilial(filialId);
        }

        public async Task<List<LazerMaster>> AllLazeroloq()
        {
            return await _lazerMasterDAL.AllLazeroloq();
        }
    }
}
