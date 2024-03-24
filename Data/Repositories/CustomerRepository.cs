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
        public async Task<List<Customer>> GetFemaleList()
        {
            return await _db.Customers.Where(x=>x.Female==true && x.IsDeactive==false).ToListAsync();
        }


        public async Task<Customer> SelectedCustomer(int CustomerId)
        {
            return await _db.Customers.Include(x => x.LazerAppointments).ThenInclude(x => x.LazerMaster).Include(x => x.LazerAppointments)
                .ThenInclude(x=>x.LazerAppointmentReports).ThenInclude(x=>x.LazerCategory).Where(x => x.Id == CustomerId && x.IsDeactive==false).FirstOrDefaultAsync();
        }

        public async Task<List<Customer>> GetMaleList()
        {
            return await _db.Customers.Where(x=>x.Female==false && x.IsDeactive==false).ToListAsync();
        }

        public async Task<List<Customer>> DailyBirthDate(int FilialId)
        {
            DateTime date = DateTime.Today;

            return await _db.Customers.Where(x => x.BirthDate.Month == date.Month && x.BirthDate.Day == date.Day && x.FilialId==FilialId && x.IsDeactive==false).ToListAsync();
        }

        public async Task<List<Customer>> GetActiveCustomerList()
        {
            return await _db.Customers.Include(x=>x.Filial).Where(x=>x.IsDeactive==false).ToListAsync();
        }
    }
}
