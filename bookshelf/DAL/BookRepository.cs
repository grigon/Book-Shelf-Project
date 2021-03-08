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
        public async Task<Book[]> GetAll(int j)
        {
            _logger.LogInformation($"Getting all Books");
            
            var genres = _context.Books.Select(b => b.Genre).Distinct();

            // var genres = _context.Genres;
            
            var books =
                from genre in genres
                from book in _context.Books.Include(b => b.Genre)/*.Include(b => b.Reviews)
                    .ThenInclude(u => u.User).Include(n => n.BookISBNs)*/.Include(b => b.Author).Where(b => b.Genre == genre)
                    .Take(2)
                select book;

            return await books.ToArrayAsync();
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
                .Include(r => r.Reviews).ThenInclude(u => u.User).Where(b => b.Genre.Name == genre).Skip(page == 1 ? 0 : page * 2 - 2).Take(2);
            
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