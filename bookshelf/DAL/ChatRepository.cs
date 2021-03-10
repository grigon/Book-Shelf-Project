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
        private readonly BaseDbContext _context;
        private readonly ILogger<ChatRepository> _logger;

        public ChatRepository(BaseDbContext context, ILogger<ChatRepository> logger)
        {
            this._context = context;
            this._logger = logger;

        }

        public void Create<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object {entity.GetType()} to the context");
            _context.Add(entity);
            //_context.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating an object {entity.GetType()} from the context");
            _context.Update(entity);
            
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing and object {entity.GetType()} from the context");
            _context.Remove(entity);
            
        }
        public async Task<bool> SaveChanges()
        {
            _logger.LogInformation("Attempting to save this changes");

            return (await _context.SaveChangesAsync() > 0);
        }



        /// <summary>
        /// for admin get all message. Is it allow??
        /// </summary>
        /// <returns></returns>

        //public async Task<ChatMessage[]> GetAll()
        //{
        //    _logger.LogInformation("Get all chat message");

        //    IQueryable<ChatMessage> query = _context.Messages.
        //    Include(autor => autor.MessageAuthor)
        //    .OrderBy(t => t.MessageDate);

        //    return await query.ToArrayAsync();
        //}
        /// <summary>
        /// for admin
        /// </summary>
        /// <returns></returns>
        public async Task<Chat[]> AllChatsForAdmin()
        {
            _logger.LogInformation($"Get all Chats for user");

            var query = _context.Chats;

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// for user return all message in chat
        /// add lazy lodaing the last 20 messages????
        /// skip to path  // additional pagination
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<ChatMessage[]> GetAllMessagesForChat(Guid id)
        {

            _logger.LogInformation("Get all chat message for one user");

            IQueryable<ChatMessage> query = _context.Messages.
                Include(autor => autor.MessageAuthor);


            query = query.Where(u => u.Chat.ChatId == id).OrderBy(m => m.MessageDate);

            return await query.ToArrayAsync();
        }

        /// <summary>
        /// //allchats/user ?????????
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ChatMessage[]> AllChatUser(Guid id)
        {
            _logger.LogInformation($"Get all Chat by id");



            IQueryable<ChatMessage> query = _context.Messages
                .Include(m => m.Chat).Where(m => m.MessageAuthor.Id == id.ToString()).Distinct();
            //join with userChat

            return await query.ToArrayAsync();

        }
        /// <summary>
        /// all chat for specific user/ logged user
        /// this to improve join , group join
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
       public async Task<ChatUser[]> AllChatIdByUserId(string userId)
        {
            IQueryable<ChatUser> query = _context.ChatUsers
                .Include(c => c.Chat)
                .Include(u => u.User);


            query = query.Where(u => u.User.Id == userId);



            //IQueryable<ChatUser> uuuser = _context.ChatUsers.Join
            //    (_context.Users,  chat => chat. ,user => user.User.Id)

            //var finalquery = _context.ChatUsers.GroupJoin(query,
            //            user => new { user.User.UserName },
            //            user2 => new { user2.User.UserName },
            //            (user, user2) =>
            //            new
            //            {
            //                user.User.UserName,
            //            });


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
        public async Task<User> GetUserById(string id)
        {
            _logger.LogInformation("Get user by Id");

            var query = _context.Users.Where(u => u.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Chat> GetChatById(Guid chatId)
        {
            var query = _context.Chats.Where(c => c.ChatId == chatId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ChatMessage> GetMessageById(string userId, Guid messageId)
        {

            var query = _context.Messages
                .Include(c => c.Chat)
                .Where(m => m.Id == messageId && m.MessageAuthor.Id == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}