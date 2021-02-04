using bookshelf.Model.Users;
using System;

namespace bookshelf.Model.Chats
{
    public class ChatUser
    {
        public Guid Id { get; set; }
        public Chat Chat { get; set; }
        public User User { get; set; }
    }
}