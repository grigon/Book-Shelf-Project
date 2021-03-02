using System;
using System.Collections;
using System.Threading.Tasks;
using bookshelf.Context;
using bookshelf.Model.Books;
using bookshelf.Model.Chats;
using Microsoft.Extensions.Logging;

namespace bookshelf.DAL
{
    public class ChatRepository : IChatRepository<Chat>
    {
        private readonly BaseDBContext _context;
        private readonly ILogger _logger;

        public ChatRepository(BaseDBContext context, ILogger logger)
        {
            this._context = context;
            this._logger = logger;

        }


        public void Create(Chat entity)
        {
            _logger.LogInformation($"Adding an object {entity.GetType()} to the context");
        }

        public void Delete(Chat entity)
        {
            throw new NotImplementedException();
        }

        public Task<Chat[]> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Chat> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChanges()
        {
            _logger.LogInformation("Attempting to save this changes");

            return (await _context.SaveChangesAsync() > 0) ;
        }

        public Task<Chat> Update(Chat t)
        {
            throw new NotImplementedException();
        }
    }
}