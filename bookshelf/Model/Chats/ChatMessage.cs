using bookshelf.Model.Users;
using System;

namespace bookshelf.Model.Chats
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public DateTime MessageDate { get; set; }
        public string Message { get; set; }
        public User MessageAuthor { get; set; }
        public Chat Chat { get; set; }
    }
}