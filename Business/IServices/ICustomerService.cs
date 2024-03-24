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
        public Task<List<Customer>> GetFemaleList();

        public Task<List<Customer>> MaleList();

        public Task<Customer> SelectedCustomer(int id);

        public Task<List<Customer>> DailyBirthDate(int FilialId);

        public  Task<List<Customer>> GetActiveCustomerList();
    }
}
