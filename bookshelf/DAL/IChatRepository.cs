using bookshelf.Model.Chats;
using bookshelf.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookshelf.DAL
{
    public interface IChatRepository 
    {
        void Create<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;

        Task<bool> SaveChanges();
        //Task<ChatMessage[]> GetAll();
        Task<ChatMessage[]> GetAllMessagesForChat(Guid id);
        Task<ChatMessage[]> AllChatUser(Guid id);
        Task<ChatMessage> GetMessageById(string userId, Guid messageId);

        Task<Chat[]> AllChatsForAdmin();
        Task<Chat> GetChatById(Guid chatId);

        Task<ChatUser[]> AllChatIdByUserId(string userId);
        Task<ChatUser[]> GetAllChatsUser();
        Task<User> GetUserById(string id);

        

    }
}
