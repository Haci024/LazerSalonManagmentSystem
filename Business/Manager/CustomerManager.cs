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
    public class CustomerManager : ICustomerService
    {
       private readonly ICustomerDAL _customerAccountDAL;
        public CustomerManager(ICustomerDAL customerDAL)
        {
            _customerAccountDAL = customerDAL;
        }
        public void Create(Customer t)
        {
            _customerAccountDAL.Create(t);
        }

        public void Delete(Customer t)
        {
            _customerAccountDAL.Delete(t);
        }

        public Customer GetById(int id)
        {
           return _customerAccountDAL.GetById(id);
        }

        public async Task<List<Customer>> GetFemaleList()
        {
            return await _customerAccountDAL.GetFemaleList();
        }

        public List<Customer> GetList()
        {
            return _customerAccountDAL.GetList();
        }


        public void Update(Customer t)
        {
           
            _customerAccountDAL.Update(t);
        }
        public async Task<Customer> SelectedCustomer(int id)
        {
            return await _customerAccountDAL.SelectedCustomer(id);
        }

        public async Task<List<Customer>> MaleList()
        {
            return await _customerAccountDAL.GetMaleList();
        }

        public async Task<List<Customer>> DailyBirthDate(int FilialId)
        {
            return await _customerAccountDAL.DailyBirthDate(FilialId);
        }
        public async Task<List<Customer>> GetActiveCustomerList()
        {
            return await _customerAccountDAL.GetActiveCustomerList();
        }
    }
}
