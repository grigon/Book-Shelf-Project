using Microsoft.EntityFrameworkCore;

namespace bookshelf.Context
{
    public class BaseDBContext : DbContext
    {
        //Connctionstring
        public BaseDBContext()
        {
            //singleton
            //przekazac connection string
        }
    }
}