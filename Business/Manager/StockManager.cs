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
    public class StockManager : IStockService
    {
        private readonly IStockDAL _stockDAL;
        public StockManager(IStockDAL stockDAL)
        {
            _stockDAL = stockDAL;
        }
        public void Create(Stock t)
        {
            _stockDAL.Create(t);
        }

        public void Delete(Stock t)
        {
            _stockDAL.Delete(t);
        }

        public Stock GetById(int id)
        {
           return _stockDAL.GetById(id);
        }

        public List<Stock> GetList()
        {
            return _stockDAL.GetList();
        }

        public void Update(Stock t)
        {
            _stockDAL.Update(t);
        }
    }
}
