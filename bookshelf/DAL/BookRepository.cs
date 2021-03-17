using System;
using System.Linq;
using System.Threading.Tasks;
using bookshelf.Context;
using bookshelf.Model.Books;
using bookshelf.Model.Users;
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
        
        public async Task<Book[]> GetAll()
        {
            _logger.LogInformation($"Getting all Books");
            
            var genres = _context.Genres;
            
            var books =
                from genre in genres
                from book in _context.Books.Include(b => b.Reviews).ThenInclude(r => r.User)
                    .Include(b => b.BookISBNs).Include(b => b.Genre)
                    .Include(b => b.Author).Where(b => b.Genre == genre).OrderBy(b => b.Rating)
                    .Take(10)
                select book;

            return await books.ToArrayAsync();
        }
        
        public async Task<Book[]> GetPageByGenre(string genre, int page)
        {
            _logger.LogInformation($"Getting all Books");

            var query = _context.Books.Include(a => a.Author).Include(g => g.Genre)
                .Include(i => i.BookISBNs)
                .Include(r => r.Reviews).ThenInclude(u => u.User)
                .Where(b => b.Genre.Name == genre).Skip(page == 1 ? 0 : page * 2 - 2).Take(2);
            
            return await query.ToArrayAsync();
        }
        
        public async Task<Book> GetById(Guid id)
        {
            _logger.LogInformation($"Getting book by id");

            var query = _context.Books.Include(a => a.Author).
                Include(g => g.Genre).Include(i => i.BookISBNs).
                Include(r => r.Reviews).ThenInclude(u => u.User)
                .Where(b => b.Id.CompareTo(id) == 0);
      
            return await query.FirstOrDefaultAsync();
        }
        
        public async Task<Book[]> GetBySearch(string search, int page)
        {
            _logger.LogInformation($"Getting book by search");

            IQueryable<Book> query = _context.Books.Include(a => a.Author).
                Include(g => g.Genre).Include(i => i.BookISBNs).
                Include(r => r.Reviews).ThenInclude(u => u.User)
                .Where(b => b.Author.FirstName.ToUpper().Contains(search.ToUpper())
                            || b.Author.LastName.ToUpper().Contains(search.ToUpper()) ||
                            b.Genre.Name.ToUpper().Contains(search.ToUpper())||
                            b.Title.ToUpper().Contains(search.ToUpper())).Skip(page == 1 ? 0 : page * 10 - 10).Take(10);
      
            return await query.ToArrayAsync();
        }
        
        public async Task<UserBook[]> GetUserBooks(string userId)
        {
            _logger.LogInformation($"Getting all user Books");

            var genres = _context.Genres;

            var books =
                from genre in genres
                from book in _context.UserBooks.Include(b => b.Book.Reviews).ThenInclude(r => r.User)
                    .Include(b => b.Book.BookISBNs).Include(b => b.BookHistories)
                    .ThenInclude(h => h.User).Include(b => b.Book.Genre)
                    .Include(b => b.Book.Author).Where(b => b.Book.Genre == genre && b.User.Id == userId)
                    .OrderBy(b => b.Book.Rating)
                    .Take(10)
                select book;

            return await books.ToArrayAsync();
        }
        
        public async Task<UserBook[]> GetAllUserBooksByGenre(string userId, string genre, int page)
        {
            _logger.LogInformation($"Getting all user Books");

           
              
              var query =  _context.UserBooks.Include(b => b.Book)
                .ThenInclude(b => b.Author).Include(b => b.Book.Genre)
                .Include(b => b.Book.Reviews).ThenInclude(r => r.User)
                .Include(b => b.Book.BookISBNs).Include(b => b.BookHistories).ThenInclude(b => b.User)
                .Where(u => u.Book.Genre.Name == genre && u.User.Id == userId)
                .Skip(page == 1 ? 0 : page * 2 - 2).Take(2);
            
            return await query.ToArrayAsync();
        }

        public async Task<UserBook> GetUserBookById(string userId, Guid Id)
        {
            _logger.LogInformation($"Getting user Book by id");

            var query =
                _context.UserBooks.Include(b => b.Book.Reviews).ThenInclude(r => r.User)
                    .Include(b => b.Book.BookISBNs)
                    .Include(b => b.Book)
                    .ThenInclude(b => b.Author).Include(b => b.Book.Genre)
                    .Include(b => b.BookHistories).ThenInclude(b => b.User)
                    .Where(u => u.User.Id == userId && u.Id.CompareTo(Id) == 0);
            
            return await query.FirstOrDefaultAsync();
        }
        
        
        public async Task<UserBook[]> GetAllUserBooksByCity(Guid bookId, int page, string city)
        {
            _logger.LogInformation($"Getting all user Books");

            var query =
                _context.UserBooks
                    .Include(b => b.Book.Reviews).ThenInclude(r => r.User)
                    .Include(b => b.Book.BookISBNs)
                    .Include(u => u.User)
                    .Include(b => b.Book)
                    .ThenInclude(b => b.Author).Include(b => b.Book.Genre)
                    .Where(/*u => u.IsPublic &&  */u => u.Book.Id.CompareTo(bookId) == 0 &&
                                u.User.City.ToUpper().Contains(city.ToUpper()))
                    .Skip(page == 1 ? 0 : page * 10 - 10).Take(10);
            
            return await query.ToArrayAsync();
        }

        public async void AddReview(Review review)
        {
            await _context.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChanges()
        {
            _logger.LogInformation("Attempting to save this changes");

            var result = (await _context.SaveChangesAsync() > 0);
            return /*(await _context.SaveChangesAsync() > 0);*/result;
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
    }
}