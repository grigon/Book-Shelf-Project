using System;

namespace bookshelf.Entities
{
    public class ChatUser
    {
        public Guid Id { get; set; }
        public Chat Chat { get; set; }
        public User User { get; set; }
    }
}