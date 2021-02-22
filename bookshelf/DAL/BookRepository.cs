using System;
using System.Linq;
using System.Threading.Tasks;
using bookshelf.Context;
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
        
        //for not logged/registered user
        public async Task<Book[]> GetAll()
        {
            _logger.LogInformation($"Getting all Books");

            IQueryable<Book> query = _context.Books.Include(a => a.Author).
                Include(g => g.Genre).Include(i => i.BookISBNs).
                Include(r => r.Reviews).ThenInclude(u => u.User);
            
            return await query.ToArrayAsync();
        }

        //for not logged/registered user
        public async Task<Book> GetById(Guid id)
        {
            _logger.LogInformation($"Getting book by id");

            IQueryable<Book> query = _context.Books.Include(a => a.Author).
                Include(g => g.Genre).Include(i => i.BookISBNs).
                Include(r => r.Reviews).ThenInclude(u => u.User).Where(b => b.Id.CompareTo(id) > 0);
      
            return await query.FirstOrDefaultAsync();
        }

        public void Add(Book t)
        {
            throw new NotImplementedException();
        }

        public Task<Book> Update(Book t)
        {
            throw new NotImplementedException();
        }

        public void Remove(Book book)
        {
            
            _context.UserBooks.Remove(userBook);
        }

        public async Task<bool> Commit()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}