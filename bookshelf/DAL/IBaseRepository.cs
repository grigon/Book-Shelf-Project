using System;
using System.Collections;
using bookshelf.Model.Books;

namespace bookshelf.DAL
{
    public interface IBaseRepository<T>
    {
        IEnumerable GetAll();
        T GetById(Guid id);
        T Add(T t);
        T Update(T t);
        T Remove(Guid id);
        int Commit();
        T UpdateIsPublic(T t);
    }
}