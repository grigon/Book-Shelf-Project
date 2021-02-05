using System;
using System.Collections;
using bookshelf.Model.Books;

namespace bookshelf.DAL
{
    public interface IBaseRepository<T>
    {
        IEnumerable GetAll();
        //T GetById(int id);
        T GetById(Guid id);
        void Add(T t);
        UserBook Update(T t);
        void Remove(int id);
        void Save();
        int Commit();
    }
}