using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IPirsinqCategoriesService:IGenericService<PirsinqCategory>
    {
        public Task<List<PirsinqCategory>> GetFemaleCategoryList();

        public Task<List<PirsinqCategory>> GetMaleCategoryList();
    }
}
