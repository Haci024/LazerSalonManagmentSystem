using Data.Concrete;
using Data.DAL;

namespace Data.Repositories
{

    public class GenericRepository<T> : IGenericDAL<T> where T : class
    {
        public void Create(T t)
        {
            using AppDbContext dbContext = new AppDbContext();
            dbContext.Set<T>().Add(t);
            dbContext.SaveChanges();
        }

        public void Delete(T t)
        {
            using AppDbContext dbContext = new AppDbContext();
            dbContext.Set<T>().Remove(t);
            dbContext.SaveChanges();

        }

        public T GetById(int id)
        {
            using AppDbContext dbContext = new AppDbContext();

            return dbContext.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            using AppDbContext dbContext = new AppDbContext();

            return dbContext.Set<T>().ToList();
        }


        public void Update(T t)
        {
            using AppDbContext appDbContext = new AppDbContext();
            
            appDbContext.Set<T>().Update(t);
            appDbContext.SaveChanges();

        }

    }
}

