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
    public class SolariumAppointmentRepository : GenericRepository<SolariumAppointment>, ISolariumAppointmentDAL
    {
        public List<SolariumAppointment> DailySolariumReportList()
        {
            AppDbContext db=new AppDbContext();

            return db.SolariumAppointments.Include(x=>x.Customer).Include(x=>x.SolariumUsingList).Include(x=>x.SolariumCategories).ThenInclude(x=>x.MainCategory).ToList();
        }
        
    }
}
