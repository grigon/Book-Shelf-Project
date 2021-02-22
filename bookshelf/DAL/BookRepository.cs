using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Results;
using bookshelf.Context;
using bookshelf.DTO.Book;
using bookshelf.Model.Books;
using bookshelf.Model.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

        //for not logged/registered user
        public async Task<Book[]> GetAll()
        {
            _logger.LogInformation($"Getting all Books");

            IQueryable<Book> query = _context.Books.Include(a => a.Author).
                Include(g => g.Genre).
                Include(r => r.Reviews).ThenInclude(u => u.User);
            
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

        public async Task<bool> Commit()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}