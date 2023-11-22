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
    public class KassaManager : IKassaService
    {
        private readonly IKassaDAL _kassaDAL;
        public KassaManager(IKassaDAL kassaDAL)
        {
            _kassaDAL = kassaDAL;
        }
        public void Create(Kassa t)
        {
            _kassaDAL.Create(t);
        }

        public void Delete(Kassa t)
        {
            _kassaDAL.Delete(t);
        }

        public Kassa GetById(int id)
        {
            return _kassaDAL.GetById(id);
        }

        public List<Kassa> GetList()
        {
            return _kassaDAL.GetList();
        }

        public void Update(Kassa t)
        {
            _kassaDAL.Update(t);
        }
    }
}
