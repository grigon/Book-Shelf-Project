using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper.Internal;
using bookshelf.Context;
using bookshelf.DTO.Book.BookLogged;
using bookshelf.Model.Books;
using bookshelf.Model.Chats;
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
        
        //for not logged/registered user
        //usunac paging
        public async Task<Book[]> GetAll(int j)
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
        
        //for not logged/registered user
        public async Task<Book> GetById(Guid id)
        {
            _logger.LogInformation($"Getting book by id");

            IQueryable<Book> query = _context.Books.Include(a => a.Author).
                Include(g => g.Genre).Include(i => i.BookISBNs).
                Include(r => r.Reviews).ThenInclude(u => u.User)
                .Where(b => b.Id.CompareTo(id) > 0);
      
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
        
        //returns all books belonging to the logged user by genre
        public async Task<UserBook[]> GetAllUserBooksByGenre(string id, string genre, int page)
        {
            _logger.LogInformation($"Getting all user Books");

            IQueryable<UserBook> query =
                _context.UserBooks
                .Include(b => b.User)
                .Include(b => b.Book)
                .ThenInclude(b => b.Author).Include(b => b.Book.Genre)
                .Include(b => b.Book.Reviews).ThenInclude(r => r.User)
                .Include(b => b.Book.BookISBNs).Include(b => b.BookHistories)
                .Where(u => u.Book.Genre.Name == genre && u.User.Id == id && u.Book.Genre.Name == genre ).Skip(page == 1 ? 0 : page * 2 - 2).Take(2);
            
            return await query.ToArrayAsync();
        }
        
        //Not logged user paging by genre
        public async Task<Book[]> GetPartByGenre(int page, string genre)
        {
            _logger.LogInformation($"Getting all Books");
            
            IQueryable<Book> query = _context.Books.Include(a => a.Author).Include(g => g.Genre)
                .Include(i => i.BookISBNs)
                .Include(r => r.Reviews).ThenInclude(u => u.User)
                .Where(b => b.Genre.Name == genre).Skip(page == 1 ? 0 : page * 10 - 10).Take(10);
            
            return await query.ToArrayAsync();
        }
        
       //logged user his books limit for all genres
        public async Task<UserBook[]> GetAllUserBooksForAllGenres(string id)
        {
            _logger.LogInformation($"Getting all user Books");

            var genres = _context.Genres;

            var books =
                from genre in genres
                from book in _context.UserBooks.Include(b => b.Book.Reviews).ThenInclude(r => r.User)
                    .Include(b => b.Book.BookISBNs).Include(b => b.BookHistories).Include(b => b.Book.Genre)
                    .Include(b => b.Book.Author).Where(b => b.Book.Genre == genre && b.User.Id == id)
                    .OrderBy(b => b.Book.Rating)
                    .Take(10)
                select book;

            return await books.ToArrayAsync();
        }
        
        public async Task<Book[]> GetBySearch(string search, int page)
        {
            _logger.LogInformation($"Getting book by id");

            IQueryable<Book> query = _context.Books.Include(a => a.Author).
                Include(g => g.Genre).Include(i => i.BookISBNs).
                Include(r => r.Reviews).ThenInclude(u => u.User)
                .Where(b => b.Author.FirstName.ToUpper().Contains(search.ToUpper())
                            || b.Author.LastName.ToUpper().Contains(search.ToUpper()) ||
                            b.Genre.Name.ToUpper().Contains(search.ToUpper())||
                            b.Title.ToUpper().Contains(search.ToUpper())).Skip(page == 1 ? 0 : page * 2 - 2).Take(2);
      
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