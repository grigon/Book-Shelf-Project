using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using bookshelf.Context;
using bookshelf.Model.Books;
using bookshelf.Model.Chats;
using bookshelf.Model.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace bookshelf.DAL
{
    public class ChatRepository : IChatRepository
    {
        private readonly BaseDBContext _context;
        private readonly ILogger<ChatRepository> _logger;

        public ChatRepository(BaseDBContext context, ILogger<ChatRepository> logger)
        {
            this._context = context;
            this._logger = logger;

        }

        public void Create<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object {entity.GetType()} to the context");
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating an object {entity.GetType()} from the context");
            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing and object {entity.GetType()} from the context");
            _context.Remove(entity);
            _context.SaveChanges();
        }
        public async Task<bool> SaveChanges()
        {
            _logger.LogInformation("Attempting to save this changes");

            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<ChatMessage[]> GetAll()
        {
            _logger.LogInformation("Get all chat message");

            IQueryable<ChatMessage> query = _context.Messages.
            Include(autor => autor.MessageAuthor)
            .OrderBy(t => t.MessageDate);

            return await query.ToArrayAsync();
        }

        public async Task<ChatMessage[]> GetAllMessagesForUsers(Guid id)
        {
            _logger.LogInformation("Get all chat message for one user");

            var query = _context.Messages.Where(u => u.MessageAuthor.Id == id).OrderBy(m => m.MessageDate);

            return await query.ToArrayAsync();
        }

        //allchats/user
        public async Task<ChatMessage[]> AllChatUser(Guid id)
        {
            _logger.LogInformation($"Get all Chat by id");

            IQueryable<ChatMessage> query = _context.Messages
                .Include(m => m.Chat).Where(m => m.MessageAuthor.Id == id);
            //join with userChat

            return await query.ToArrayAsync();

        }
        //allchats/admin
        public async Task<Chat[]> AllChatsForAdmin()
        {
            _logger.LogInformation($"Get all Chats for user");

            var query = _context.Chats;

            return await query.ToArrayAsync();
        }

        public async Task<ChatMessage[]> MessagesForOneChat(Guid chatid)
        {
            _logger.LogInformation("Get all messages for one chat");

            var query = _context.Messages.Where(c => c.Chat.ChatId == chatid);


            return await query.ToArrayAsync();
        }

        //public async Task<Chat> GetChatIdToConversation()
        //{

        //    var chat = _context.Chats.

        //    return await chat.FirstOrDefaultAsync();
        //}

        public async Task<ChatUser[]> GetAllChatsUser()
        {
            _logger.LogInformation("Get all messages for one chat");

            var query = _context.ChatUsers
                        .Include(c => c.Chat)
                        .Include(u => u.User);

            return await query.ToArrayAsync();

        }
        public async Task<User> GetUserById(Guid id)
        {
            _logger.LogInformation("Get user by Id");

            var query = _context.Users.Where(u => u.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}