using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface ICustomerService : IGenericService<Customer>
    {
        public List<Customer> GetFemaleList();

        public List<Customer> MaleList();

        public Customer SelectedCustomer(int id);
    }
}
