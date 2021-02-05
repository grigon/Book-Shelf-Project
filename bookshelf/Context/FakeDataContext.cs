using System;
using Microsoft.EntityFrameworkCore;

namespace bookshelf.Context
{
    public class FakeDataContext : DbContext, IBaseContext
    {
        public FakeDataContext(DbContextOptions<FakeDataContext> options)
            : base(options)
        {
            Connect();
        }
        
        public void Connect()
        {
            
            Console.WriteLine(dupa());
        }
        
        public string dupa()
        {
            return "dupa";
        }
    }
}