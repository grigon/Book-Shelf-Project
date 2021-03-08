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
        
        public Guid ChatId { get; set; }
        
        public Guid UserId { get; set; }
    }
}
