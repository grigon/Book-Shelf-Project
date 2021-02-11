using System;
using System.Collections;
using bookshelf.Model.Books;

namespace bookshelf.DAL
{
    public interface IBaseRepository<T>
    {
        IEnumerable GetAll();
        T GetById(Guid id);
        void Add(T t);
        UserBook Update(T t);
        void Remove(Guid id);
        void Save();
        UserBook UpdateIsPublic(T t);
        int Commit();
    }
}