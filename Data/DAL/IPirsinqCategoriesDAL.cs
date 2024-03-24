using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public interface IPirsinqCategoriesDAL : IGenericDAL<PirsinqCategory>
    {
        public  Task<List<PirsinqCategory>> GetFemaleCategories();
        public  Task<List<PirsinqCategory>> GetMaleCategories();
    }
}
