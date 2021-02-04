using Microsoft.EntityFrameworkCore;

namespace bookshelf.Context
{
    public class BaseDBContext : DbContext
    {
        //Connctionstring
        public BaseDBContext()
        {
            //pod context można na podpiąć każdą strukturę, 
            //singleton
            //przekazac connection string
        }
    }
}