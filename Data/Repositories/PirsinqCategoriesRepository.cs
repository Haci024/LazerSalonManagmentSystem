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
    public class PirsinqCategoriesRepository : GenericRepository<PirsinqCategory>, IPirsinqCategoriesDAL
    {
        private readonly AppDbContext _db;
        public PirsinqCategoriesRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<PirsinqCategory>> GetFemaleCategories()
        {
            return await _db.PirsinqCategories.Include(x=>x.MainCategory).Where(x=>x.MainCategoryId==1 && x.IsDeactive==false).ToListAsync();
        }

        public async Task<List<PirsinqCategory>> GetMaleCategories()
        {
            return await _db.PirsinqCategories.Include(x => x.MainCategory).Where(x => x.MainCategoryId == 2 && x.IsDeactive==false).ToListAsync();
        }
    }
}
