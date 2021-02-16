using System;
using bookshelf.Model.Books;
using bookshelf.Model.Chats;
using bookshelf.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace bookshelf.Context
{
    public class BaseDBContext : DbContext
    {
        private readonly string _connectionString;

        public BaseDBContext(DbContextOptions<BaseDBContext> options)
            : base(options)
        {
        }

        
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<BookISBN> BookISBN { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<BookHistory> BookHistory { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}