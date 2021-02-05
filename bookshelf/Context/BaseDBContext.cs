using System;
using Microsoft.EntityFrameworkCore;

namespace bookshelf.Context
{
    public class BaseDBContext : DbContext, IBaseContext
    {

        public BaseDBContext(DbContextOptions<BaseDBContext> options)
            : base(options)
        {
        }

        public void Connect()
        {
        }


        
    }
}