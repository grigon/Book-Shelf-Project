using System;
using System.Collections;
using System.Threading.Tasks;
using bookshelf.Model.Books;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Identity;

namespace bookshelf.DAL
{
    public interface IUserRepository<T> where T : class

    {
        Task<T[]> GetAll();
        Task<T> GetById(Guid id);
        void Add(T t);
        Task<T> Update(T t);
        void Remove(T entity);
        Task<bool> Commit();
        // Task<IdentityUserToken<string>> GetRefreshTokenById(string id);
    }
}