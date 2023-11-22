using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IGenericService<T> where T : class
    {
        void Create(T t);
        void Update(T t);
        void Delete(T t);
        public T GetById(int id);
        public List<T> GetList();

    }
}
