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

        Task<ChatMessage[]> GetAll();
        Task<ChatMessage[]> GetAllMessagesForChat(Guid id);
        Task<ChatMessage[]> AllChatUser(Guid id);

        Task<Chat[]> AllChatsForAdmin();
        Task<ChatUser[]> AllChatIdByUserId(Guid UserId);
        //Task<Chat> GetChatIdToConversation();
        Task<ChatUser[]> GetAllChatsUser();

        Task<Chat> GetChatById(Guid chatId);
        Task<bool> SaveChanges();

        Task<User> GetUserById(Guid id);

        

    }
}
