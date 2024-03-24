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
    public class CosmetologyAppointmentRepository : GenericRepository<CosmetologyAppointment>, ICosmetologyAppointmentDAL
    {
        private readonly AppDbContext _db;
        public CosmetologyAppointmentRepository(AppDbContext db)
        {
            _db = db;   
        }
        public async Task<List<CosmetologyAppointment>> CosmetologReservation()
        {
            return await _db.CosmetologyAppointments.Include(x => x.Cosmetolog).Include(x=>x.Customers).Include(x => x.AppUser).Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory).ThenInclude(x=>x.MainCategory).Where(x=>x.IsCompleted==false).ToListAsync();
        }
        public async Task<CosmetologyAppointment> SelectedAppointment(int appointmentId)
        {
            return await _db.CosmetologyAppointments.Include(x => x.Customers).Include(x => x.Cosmetolog).Include(x=>x.CosmetologyReports).ThenInclude(x=>x.CosmetologyCategory).ThenInclude(x=>x.MainCategory).ThenInclude(x=>x.ChildCategory).Include(x=>x.AppUser).Include(x => x.Filial).FirstOrDefaultAsync(x=>x.Id==appointmentId);
        }
    }
}
