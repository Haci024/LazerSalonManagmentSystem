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
    public class CosmetologCategoryRepository : GenericRepository<CosmetologyCategory>, ICosmetologCategoryDAL
    {
        private readonly AppDbContext _db;
        public CosmetologCategoryRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<CosmetologyCategory>> GetAllCosmetologCategory()
        {
           return await  _db.CosmetologyCategories.Include(x=>x.MainCategory).Include(x=>x.ChildCategory).Where(x=>x.IsDeactive==false && x.MainCategoryId!=null).ToListAsync();    
        }
    }
}
