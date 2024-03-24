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
    public class LipuckaCategoriesRepository:GenericRepository<LipuckaCategories>,ILipuckaCategoriesDAL
    {
        private readonly AppDbContext _db;
        public LipuckaCategoriesRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<LipuckaCategories>> GetFemaleCategoryList()
        {
           return await _db.LipuckaCategories.Include(x=>x.MainCategory).Where(x=>x.IsDeactive==false && x.MainCategoryId==1).ToListAsync();
        }

        public async Task<List<LipuckaCategories>> GetMaleCategoryList()
        {
            return await _db.LipuckaCategories.Include(x => x.MainCategory).Where(x => x.IsDeactive == false && x.MainCategoryId == 2).ToListAsync();
        }
    }
}
