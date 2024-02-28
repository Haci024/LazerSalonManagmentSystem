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
    public class BodyShapingPacketCategoriesManager : IBodyShapingPacketCategoriesService
    {
        private readonly IBodyShapingPacketCategoriesDAL _dal;
        public BodyShapingPacketCategoriesManager(IBodyShapingPacketCategoriesDAL dal)
        {
            _dal = dal;
        }
        public void Create(BodyShapingPacketCategory t)
        {
            _dal.Create(t);
        }

        public void Delete(BodyShapingPacketCategory t)
        {
           _dal.Delete(t);
        }

        public BodyShapingPacketCategory GetById(int id)
        {
           return _dal.GetById(id);
        }

        public List<BodyShapingPacketCategory> GetList()
        {
            return _dal.GetList();
        }

        public void Update(BodyShapingPacketCategory t)
        {
            _dal.Update(t);
        }
    }
}
