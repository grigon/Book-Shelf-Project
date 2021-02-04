using System;
using System.Collections;

namespace bookshelf.DAL
{
    public interface IBaseRepository<T>
    {
        IEnumerable GetAll();
        //T GetById(int id);
        T GetById(Guid id);
        void Add(T t);
        void Update(T t);
        void Remove(int id);
        void Save();
    }
}