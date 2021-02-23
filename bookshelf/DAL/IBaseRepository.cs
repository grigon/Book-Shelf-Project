using System;
using System.Threading.Tasks;

namespace bookshelf.DAL
{
    public interface IBaseRepository<T> where T : class

    {
        Task<T[]> GetAll();
        Task<T> GetById(Guid id);
        void Add(T entity);
        Task<T> Update(T t);
        void Remove(T entity);
        Task<bool> Commit();
    }
}