using bookshelf.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookshelf.DTOS
{
    public class ChatMessageDTO
    {
        public DateTime MessageDate { get; set; }
        public string Message { get; set; }
        
        public string Autor { get; set; }
        
    }
}
