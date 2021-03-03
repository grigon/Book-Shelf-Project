using System;
using System.Linq;
using System.Threading.Tasks;
using bookshelf.Context;
using bookshelf.Model.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace bookshelf.DAL
{
    public class BookRepository
    {

        private readonly BaseDbContext _context;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(BaseDbContext context, ILogger<BookRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        //for not logged/registered user
        public async Task<Book[]> GetAll(int j/*string genre*/)
        {
            _logger.LogInformation($"Getting all Books");

            IQueryable<Genre> genres = _context.Genres;
            Genre[] gg = genres.ToArray();

            IQueryable<Book> query = _context.Books.Include(a => a.Author).Include(g => g.Genre)
                .Include(i => i.BookISBNs)
                .Include(r => r.Reviews).ThenInclude(u => u.User)/*.Where(b => b.Genre.Name == genre)*/.Take(2);
            
            foreach (var g in gg)
            {
                query = query.Concat(_context.Books.Include(a => a.Author).Include(g => g.Genre)
                    .Include(i => i.BookISBNs)
                    .Include(r => r.Reviews).ThenInclude(u => u.User).Where(b => b.Genre.Name == g.Name).Skip(j).Take(2));
            }

            /*
            var query2 = from genre in _context.Genres
                join book in _context.Books on
                    genre.Name equals book.Genre.Name into bookGroup
                select new
                {
                    Genre = genre,
                    Book = bookGroup
                };
            var query3 = _context.Genres.GroupJoin(_context.Books,
                g => g.Name,
                b => b.Genre.Name,
                (g, b) =>
                    new
                    {
                        Genge = g,
                        Book = b
                    }).GroupBy(g => g.Genge.Name);
            var query4 = query3.SelectMany(b => b.SelectMany(g => g.Book).Take(2));
            
            foreach (var gb in query3)
            {
              foreach (var b in gb)
                    {
                        
                    }
            }*/

            /*
            var queryA = from book in _context.Books
                group book by book.Genre.Name
                into bookGroup
                select new
                {
                    Book = bookGroup
                } into result orderby result.Book.Key select result;*/
            
            
            /*
            var query3 = _context.Genres.Join(_context.Books,
                g => g.Name,
                b => b.Genre.Name,
                (g, b) =>
                    new
                    {
                        Genge = g,
                        Book = b
                    }).GroupBy(g => g.Genge.Name).Select(b => b);
            
            var a = 
                _context.Genres.GroupJoin(_context.Books,
                    genre => genre.Name,
                    book => book.Genre.Name,
                    (g, booksCollection) =>
                        new
                        {
                            Books = booksCollection.Select(book => book).Take(2)
                        });*/
            
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

        public async Task<Book[]> GetAllLoogged(string genre)
        {
            _logger.LogInformation($"Getting all Books");

            IQueryable<Book> query = _context.Books.Include(a => a.Author).Include(g => g.Genre)
                .Include(i => i.BookISBNs)
                .Include(r => r.Reviews).ThenInclude(u => u.User).Where(b => b.Genre.Name == genre).Take(2);
            
            return await query.ToArrayAsync();
        }

        //for not logged/registered user
        public async Task<Book> GetByIdLogged(Guid id)
        {
            _logger.LogInformation($"Getting book by id");

            IQueryable<Book> query = _context.Books.Include(a => a.Author).
                Include(g => g.Genre).Include(i => i.BookISBNs).
                Include(r => r.Reviews).ThenInclude(u => u.User).Where(b => b.Id.CompareTo(id) > 0);
      
            return await query.FirstOrDefaultAsync();
        }
        
        //returns all books belonging to the logged user
        public async Task<UserBook[]> GetAllUserBooks(Guid id, string genre)
        {
            _logger.LogInformation($"Getting all user Books");

            IQueryable<UserBook> query = _context.UserBooks.Include(b => b.Book).ThenInclude(g => g.Genre).Where(b=> b.Book.Genre.Name == genre)
                .Include(b => b.Book.BookISBNs)
                .Include(r => r.Book.Reviews).ThenInclude(u => u.User)
                .Where(u => u.User.Id.CompareTo(id) > 0);
            
            return await query.ToArrayAsync();
        }
        
        //Not logged user paging by genre
        public async Task<Book[]> GetPartByGenre(int page, string genre)
        {
            _logger.LogInformation($"Getting all Books");
            //why not correct?
            IQueryable<Book> query = _context.Books.Include(a => a.Author).Include(g => g.Genre)
                .Include(i => i.BookISBNs)
                .Include(r => r.Reviews).ThenInclude(u => u.User).Where(b => b.Genre.Name == genre).Skip(page > 1 ? 2 * page - 2 : 0).Take(2);
            
            return await query.ToArrayAsync();
        }
        
        public void Add(UserBook userBook)
        {
            _context.Add(userBook);
        }

        public Task<Book> Update(Book book)
        {
            throw new NotImplementedException();
        }

        public void Remove(UserBook userBook)
        {
            _context.UserBooks.Remove(userBook);
        }

        public async Task<bool> Commit()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}