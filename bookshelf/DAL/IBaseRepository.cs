using System;
using System.Collections;
using System.Threading.Tasks;
using bookshelf.Model.Books;

namespace bookshelf.DAL
{
    public interface IBaseRepository<T>
    {
        Task<T[]> GetAll();
        Task<T> GetById(Guid id);
        void Add(T entity);
        Task<T> Update(T t);
        void Remove(T entity);
        Task<bool> Commit();
    }
}