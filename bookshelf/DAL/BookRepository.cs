using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using bookshelf.Context;
using bookshelf.DTO.Book;
using bookshelf.Model.Books;
using Microsoft.EntityFrameworkCore;
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


        public async Task<Book[]> GetAll()
        {
            _logger.LogInformation($"Getting all Books");

            IQueryable<Book> query = _context.Books;

            return await query.ToArrayAsync();
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