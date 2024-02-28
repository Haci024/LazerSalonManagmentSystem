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
        public Task<List<Customer>> GetFemaleList();

        public Task<List<Customer>> GetMaleList();

        public Task<Customer> SelectedCustomer(int CustomerId);

        public Task<List<Customer>> DailyBirthDate(int FilialId);
    }
}
