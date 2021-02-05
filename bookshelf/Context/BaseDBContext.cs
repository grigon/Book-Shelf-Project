using bookshelf.Model.Books;
using bookshelf.Model.Chats;
using bookshelf.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace bookshelf.Context
{
    public class BaseDBContext : DbContext, IBaseContext
    {
        private readonly string _connectionString;

        public BaseDBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public void Commit()
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
        public DbSet<UserBook> BookUsers { get; set; }
        public DbSet<BookHistory> BookHistory { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}