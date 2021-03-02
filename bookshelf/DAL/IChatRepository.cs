using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookshelf.DAL
{
    public interface IChatRepository<T> where T : class
    {

        Task<T[]> GetAll();
        Task<T> GetById(Guid id);
        void Add(T entity);
        Task<T> Update(T t);
        void Remove(T entity);
        Task<bool> Commit();

    }
}
