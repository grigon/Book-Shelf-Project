using System.Collections.Generic;
using bookshelf.Context;
using bookshelf.Entities;

namespace bookshelf.DAL
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T t);
        void Update(T t);
        void Delete(int id);
        void Save();
    }
}