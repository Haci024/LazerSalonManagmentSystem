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

        public List<LazerAppointment> GetAllInjectList()
        {
            using AppDbContext db = new AppDbContext(); 
            
           return db.LazerAppointments.Include(x=>x.LazerMaster).Include(x=>x.AppUser).Include(x=>x.Customers).Include(x => x.LazerAppointmentReports).ThenInclude(x=>x.LazerCategory).ToList();
        }

        public List<LazerAppointment> LazerAppointmentsReports()
        {
           AppDbContext db=new AppDbContext();
          
        
            return db.LazerAppointments.Include(x => x.LazerMaster).Include(x=>x.AppUser).Include(x => x.Customers).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).ToList();
       
        }

        public List<LazerAppointment> SuccesfullyAppointments()
        {
            using AppDbContext db = new AppDbContext();
            
          
            return db.LazerAppointments.Include(x=>x.LazerMaster).Include(x=>x.AppUser).Include(x=>x.Customers).Include(x=>x.LazerAppointmentReports).ThenInclude(x=> x.LazerCategory).OrderBy(x=>x.ReservationDate).ToList();
        }
        public async  Task<LazerAppointment>  SelectedCustomer(int AppointmentId)
        {
            using AppDbContext db = new AppDbContext();

            return await db.LazerAppointments.Include(x => x.Customers).Include(x=>x.AppUser).Include(x => x.LazerMaster).Where(x => x.Id == AppointmentId).FirstOrDefaultAsync();
        }
        public List<LazerAppointment> NextSessionList(AppUser appUser)
        {
            DateTime today = DateTime.Today.Date;
            using AppDbContext db = new AppDbContext();
            return db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.NextSessionDate.Value.Date == today && x.FilialId==appUser.FilialId).ToList();

        }

        public List<LazerAppointment> AllReservations(AppUser appUser)
        {
            using AppDbContext db = new AppDbContext();
            return db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.IsCompleted == false && x.FilialId == appUser.FilialId).ToList();
        }
        public List<LazerAppointment> InCompletedList(AppUser appUser)
        {

            using AppDbContext db = new AppDbContext();

            return db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.IsContiued == true && x.FilialId==appUser.FilialId).ToList();
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
    }
}
