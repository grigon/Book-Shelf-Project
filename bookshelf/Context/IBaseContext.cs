using Microsoft.EntityFrameworkCore.Internal;

namespace bookshelf.Context
{
    public interface IBaseContext
    {
        public void Connect();
    }
}