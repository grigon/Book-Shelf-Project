using System;
using Microsoft.EntityFrameworkCore;

namespace bookshelf.Context
{
    public class FakeDataContext : DbContext, IBaseContext
    {
        public FakeDataContext()
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