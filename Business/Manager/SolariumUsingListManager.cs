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
    public class SolariumUsingListManager : ISolariumUsingListService
    {
        private readonly ISolariumUsingListDAL _service;
        public SolariumUsingListManager(ISolariumUsingListDAL solariumUsingListDAL)
        {
            _service = solariumUsingListDAL;
        }
        public void Create(SolariumUsingList t)
        {
            _service.Create(t);
        }

        public void Delete(SolariumUsingList t)
        {
            _service.Delete(t);
        }

        public SolariumUsingList GetById(int id)
        {
           return _service.GetById(id);
        }

        public List<SolariumUsingList> GetList()
        {
           return _service.GetList();
        }

        public void Update(SolariumUsingList t)
        {
            _service.Update(t);
        }

    }
}
