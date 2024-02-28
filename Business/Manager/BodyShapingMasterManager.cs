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
    public class BodyShapingMasterManager : IBodyShapingMasterService
    {
        private readonly IBodyShapingMasterDAL _bodyShapingMasterDAL;
        public BodyShapingMasterManager(IBodyShapingMasterDAL bodyShapingMasterDAL)
        {

            _bodyShapingMasterDAL = bodyShapingMasterDAL;

        }
        public void Create(BodyShapingMaster t)
        {
            _bodyShapingMasterDAL.Create(t);    
        }

        public void Delete(BodyShapingMaster t)
        {
            _bodyShapingMasterDAL.Delete(t);
        }

        public BodyShapingMaster GetById(int id)
        {
           return _bodyShapingMasterDAL.GetById(id);
        }

        public List<BodyShapingMaster> GetList()
        {
            return _bodyShapingMasterDAL.GetList();
        }

        public void Update(BodyShapingMaster t)
        {
            _bodyShapingMasterDAL.Update(t);
        }
    }
}
