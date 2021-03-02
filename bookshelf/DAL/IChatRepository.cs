using bookshelf.Model.Chats;
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
        Task<ChatMessage[]> GetAllMessagesForUsers(Guid id);
        Task<ChatMessage[]> GetByIdChatId(Guid id);
        Task<Chat[]> AllChatUser(Guid id);
        Task<bool> SaveChanges();

        

    }
}
