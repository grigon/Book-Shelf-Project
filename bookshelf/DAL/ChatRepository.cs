using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using bookshelf.Context;
using bookshelf.Model.Books;
using bookshelf.Model.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace bookshelf.DAL
{
    public class ChatRepository : IChatRepository
    {
        private readonly BaseDBContext _context;
        private readonly ILogger _logger;

        public ChatRepository(BaseDBContext context, ILogger logger)
        {
            this._context = context;
            this._logger = logger;

        }

        public void Create<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object {entity.GetType()} to the context");
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<ChatMessage[]> GetAll()
        {
            _logger.LogInformation("Get all chat message");

            var query =  _context.Messages.OrderBy(t => t.MessageDate);

            return await query.ToArrayAsync();
        }

        public async Task<ChatMessage[]> GetAllMessagesForUsers(Guid id)
        {
            _logger.LogInformation("Get all chat message for one user");

            var query = _context.Messages.Where(u => u.MessageAuthor.Id == id).OrderBy(m => m.MessageDate);

            return await query.ToArrayAsync();
        }
        public async Task<bool> SaveChanges()
        {
            _logger.LogInformation("Attempting to save this changes");

            return (await _context.SaveChangesAsync() > 0) ;
        }

    }
}