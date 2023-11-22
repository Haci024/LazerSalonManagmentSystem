using Data.Concrete;
using Data.DAL;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerDAL
    {
        private readonly AppDbContext _db;
        public CustomerRepository(AppDbContext db)
        {
            _db = db;
        }
        public List<Customer> GetFemaleList()
        {
            return _db.Customers.Where(x=>x.Female==true).ToList();
        }


        public  Customer SelectedCustomer(int CustomerId)
        {
            return  _db.Customers.Include(x => x.LazerAppointments).ThenInclude(x=>x.LazerMaster).Include(x=>x.LazerAppointments)
                .ThenInclude(x=>x.LazerAppointmentReports).ThenInclude(x=>x.LazerCategory).Where(x => x.Id == CustomerId).FirstOrDefault();
        }

        public  List<Customer> GetMaleList()
        {
            return _db.Customers.Where(x=>x.Female==false).ToList();
        }
        

        
    }
}
