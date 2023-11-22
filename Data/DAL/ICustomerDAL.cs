using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public interface ICustomerDAL : IGenericDAL<Customer>
    {
        public List<Customer> GetFemaleList();

        public List<Customer> GetMaleList();

        public Customer SelectedCustomer(int CustomerId);
    }
}
