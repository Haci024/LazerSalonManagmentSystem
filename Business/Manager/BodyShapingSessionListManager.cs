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
    public class BodyShapingSessionListManager : IBodyShapingSessionListService
    {
        private readonly IBodyShapingSessionListDAL _bodyShapingSessionListDAL;
        public BodyShapingSessionListManager(IBodyShapingSessionListDAL bodyShapingSessionListDAL)
        {
            _bodyShapingSessionListDAL = bodyShapingSessionListDAL;
        }
        public void Create(BodyShapingSessionList t)
        {
            _bodyShapingSessionListDAL.Create(t);
        }

        public void Delete(BodyShapingSessionList t)
        {
           _bodyShapingSessionListDAL.Delete(t);
        }

        public BodyShapingSessionList GetById(int id)
        {
            return _bodyShapingSessionListDAL.GetById(id);
        }

        public List<BodyShapingSessionList> GetList()
        {
            return _bodyShapingSessionListDAL.GetList();
        }

        public void Update(BodyShapingSessionList t)
        {
            _bodyShapingSessionListDAL.Update(t);
        }
    }
}
