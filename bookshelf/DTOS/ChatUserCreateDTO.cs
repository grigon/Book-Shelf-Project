using bookshelf.Model.Chats;
using bookshelf.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookshelf.DTOS
{
    public class ChatUserCreateDTO
    {
        
        public Guid chatId { get; set; }
        
        public Guid userId { get; set; }
    }
}
