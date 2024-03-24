using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface ILipuckaCategoriesService:IGenericService<LipuckaCategories>
    {
        public Task<List<LipuckaCategories>> GetFemaleCategoryList();

        public Task<List<LipuckaCategories>> GetMaleCategoryList();
    }
}
