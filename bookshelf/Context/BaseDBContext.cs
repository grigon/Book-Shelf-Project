using System;
using Microsoft.EntityFrameworkCore;

namespace bookshelf.Context
{
    public class BaseDBContext : DbContext, IBaseContext
    {

        /*public BaseDBContext(DbContextOptions<BaseDBContext> options)
            : base(options)
        {
        }*/
        
        private readonly string _connectionString;

        public BaseDBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public void Connect()
        {
            Console.WriteLine("db Context");
        }


        
    }
}