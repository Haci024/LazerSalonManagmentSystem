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
    public class KassaActionListManager : IKassaActionListService
    {
        private readonly IKassaActionListDAL _kassaActionListDAL;
        public KassaActionListManager(IKassaActionListDAL kassaActionListDAL)
        {
            _kassaActionListDAL = kassaActionListDAL;
        }

        public void Create(KassaActionList t)
        {
            _kassaActionListDAL.Create(t);
        }

        public void Delete(KassaActionList t)
        {
           _kassaActionListDAL.Delete(t);
        }

        public KassaActionList GetById(int id)
        {
          return _kassaActionListDAL.GetById(id);
        }

        public List<KassaActionList> GetList()
        {
           return _kassaActionListDAL.GetList();
        }

        public void Update(KassaActionList t)
        {
            _kassaActionListDAL.Update(t);
        }
    }
}
