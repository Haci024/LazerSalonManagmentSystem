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
    public class CosmetologManager:ICosmetologService
    {
        private readonly ICosmetologDAL _cosmetologDAL;
        public CosmetologManager(ICosmetologDAL cosmetologDAL)
        {
            _cosmetologDAL = cosmetologDAL;
        }

        public void Create(Cosmetologs t)
        {
            _cosmetologDAL.Create(t);
        }

        public void Delete(Cosmetologs t)
        {
            _cosmetologDAL.Delete(t);
        }

        public Cosmetologs GetById(int id)
        {
            return _cosmetologDAL.GetById(id);
        }

        public List<Cosmetologs> GetList()
        {
            return _cosmetologDAL.GetList();
        }

        public void Update(Cosmetologs t)
        {
            _cosmetologDAL.Update(t);
        }
    }
}
