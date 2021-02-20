using System;
using System.Collections;
using System.Threading.Tasks;
using bookshelf.Context;
using bookshelf.Model.Books;
using Microsoft.Extensions.Logging;

namespace bookshelf.DAL
{
    public class BookRepository : IBaseRepository<Book>
    {
        private readonly BaseDBContext _context;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(BaseDBContext context, ILogger<BookRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public Task<Book[]> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(Book t)
        {
            throw new NotImplementedException();
        }

        public Task<Book> Update(Book t)
        {
            throw new NotImplementedException();
        }

        public void Remove(Book entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Commit()
        {
            throw new NotImplementedException();
        }
    }
}