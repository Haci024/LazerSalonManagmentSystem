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
    public class LazerMasterRepository : GenericRepository<LazerMaster>, ILazerMasterDAL
    {
        private readonly AppDbContext _appDbContext;
        public LazerMasterRepository(AppDbContext db)
        {
            _appDbContext = db;
        }
        public async Task<List<LazerMaster>> GetLazeroloqListByFilial(int filialId)
        {
            return await _appDbContext.LazerMasters.Include(x => x.LazerMasterFilial).ThenInclude(x => x.Filial).Where(x => x.LazerMasterFilial.Any(x=>x.FilialId==filialId)&& x.IsDeactive==false).ToListAsync();
        }
        public async Task<List<LazerMaster>> AllLazeroloq()
        {
            return await _appDbContext.LazerMasters.Include(x=>x.LazerMasterFilial).ThenInclude(x=>x.Filial).Include(x=>x.LazerAppointment).ToListAsync();
        }
    }
}
