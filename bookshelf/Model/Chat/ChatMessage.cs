using System;

namespace bookshelf.Entities
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