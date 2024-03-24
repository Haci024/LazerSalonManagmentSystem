using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface ICosmetologCategoryService:IGenericService<CosmetologyCategory>
    {
        public Task<List<CosmetologyCategory>> GetAllCategories();
    }
}
