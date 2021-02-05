using System;
using Microsoft.EntityFrameworkCore;

namespace bookshelf.Context
{
    public class FakeDataContext : DbContext, IBaseContext
    {
        public FakeDataContext()
        {

        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}