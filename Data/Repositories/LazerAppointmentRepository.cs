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
    public class LazerAppointmentRepository : GenericRepository<LazerAppointment>, ILazerAppointmentDAL
    {

        public async Task<List<LazerAppointment>> GetAllInjectList()
        {
            using AppDbContext db = new AppDbContext(); 
            
           return await db.LazerAppointments.Include(x=>x.LazerMaster).Include(x=>x.AppUser).Include(x=>x.Customers).Include(x => x.LazerAppointmentReports).ThenInclude(x=>x.LazerCategory).ToListAsync();
        }

        public async Task<List<LazerAppointment>> LazerAppointmentsReports()
        {
           AppDbContext db=new AppDbContext();
          
        
            return await db.LazerAppointments.Include(x => x.LazerMaster).Include(x=>x.AppUser).Include(x => x.Customers).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).ToListAsync();
       
        }

        public async Task<List<LazerAppointment>> SuccesfullyAppointments(int filialId)
        {
            using AppDbContext db = new AppDbContext();
            
          
            return await db.LazerAppointments.Include(x=>x.LazerMaster).Include(x=>x.AppUser).Include(x=>x.Customers).Include(x=>x.LazerAppointmentReports).ThenInclude(x=> x.LazerCategory).OrderBy(x=>x.ReservationDate).Where(x=>x.IsCompleted == false && x.IsDeleted == false && x.FilialId==filialId).ToListAsync();
        }
        public  async Task<LazerAppointment>  SelectedCustomer(int AppointmentId)
        {
       
            using AppDbContext db = new AppDbContext();

            return await db.LazerAppointments.Include(x => x.Customers).Include(x => x.AppUser).Include(x => x.LazerMaster).FirstOrDefaultAsync(x => x.Id == AppointmentId);
        }
        public async Task<List<LazerAppointment>> NextSessionList(int filialId)
        {
            DateTime date = new DateTime();
            using AppDbContext db = new AppDbContext();
            return await db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x =>x.NextSessionDate.Value.Date==DateTime.Today && x.FilialId==filialId).ToListAsync();

        }

        public async Task<List<LazerAppointment>> AllReservations(int filialId)
        {
            using AppDbContext db = new AppDbContext();
            return await db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.IsCompleted == false && x.FilialId == filialId).ToListAsync();
        }
        public async Task<List<LazerAppointment>> InCompletedList(int filialId)
        {

            using AppDbContext db = new AppDbContext();

            return await db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x=>x.FilialId == filialId && x.IsContiued == true ).ToListAsync();
        }

        public async Task<LazerAppointment> CompletelySecondSessionEnd(int AppointmentId)
        {
           using AppDbContext db = new AppDbContext();

            return await db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Where(x => x.Id == AppointmentId).FirstOrDefaultAsync();
        }
        public async Task<LazerAppointment> CompletetedSecondSessionStart(int AppointmentId)
        {

            using AppDbContext db = new AppDbContext();

            return await db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Where(x => x.Id == AppointmentId).FirstOrDefaultAsync();
        }

        public async Task<List<LazerAppointment>> ReservationsForMaster(int filialId, int LazerMasterId)
        {
            using AppDbContext db = new AppDbContext();
            return await db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.IsCompleted == false && x.FilialId == filialId && x.LazerMasterId==LazerMasterId && x.IsDeleted==false).ToListAsync();
        }

        public async Task<List<LazerAppointment>> InjectionsForMaster(int filialId ,int lazermasterId)
        {
            using AppDbContext db = new AppDbContext();
            return await db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.IsCompleted == false && x.FilialId == filialId && x.LazerMasterId == lazermasterId && x.IsDeleted == true).ToListAsync();
        }
    }
}
