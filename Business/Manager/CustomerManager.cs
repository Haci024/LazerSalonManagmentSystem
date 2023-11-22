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

        public List<Customer> GetFemaleList()
        {
            return _customerAccountDAL.GetFemaleList();
        }

        public List<Customer> GetList()
        {
            return _customerAccountDAL.GetList();
        }


        public void Update(Customer t)
        {
           
            _customerAccountDAL.Update(t);
        }
        public Customer SelectedCustomer(int id)
        {
            return _customerAccountDAL.SelectedCustomer(id);
        }

        public List<Customer> MaleList()
        {
            return _customerAccountDAL.GetMaleList();
        }
    }
}
