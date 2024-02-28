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
    public class LazerCategoryManager : ILazerCategoryService
    {
        private readonly ILazerCategoryDAL _dal;
        public LazerCategoryManager(ILazerCategoryDAL dal)
        {

            _dal = dal;

        }
        public void Create(LazerCategory t)
        {
           _dal.Create(t);
        }

        public void Delete(LazerCategory t)
        {
           _dal.Delete(t);
        }

        public LazerCategory GetById(int id)
        {
            return _dal.GetById(id);
        }

        public List<LazerCategory> GetList()
        {
            return _dal.GetList();
        }

        public void Update(LazerCategory t)
        {
            _dal.Update(t);
        }
    }
}
