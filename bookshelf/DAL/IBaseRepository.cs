using System;
using System.Collections;
using System.Threading.Tasks;
using bookshelf.Model.Books;
using bookshelf.Model.Users;

namespace bookshelf.DAL
{
    public interface IBaseRepository<T>
    {
        Task<T[]> GetAll();
        Task<T> GetById(Guid id);
        Task<T> Add(T t);
        T Update(T t);

        void Remove(T entity);
        
        Task<bool> Commit();
    }
}