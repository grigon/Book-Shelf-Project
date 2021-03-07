using bookshelf.Model.Chats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookshelf.DTOS
{
    public class ChatMessageCreateDTO
    {
        public DateTime MessageDate { get; set; } = DateTime.Now;
        public string Message { get; set; }
        public Guid AutorId { get; set; }
        public Guid ChatId { get; set; }
    }
}
