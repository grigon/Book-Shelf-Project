using System;
using bookshelf.Entities;

namespace bookshelf
{
    public class UserBook
    {
        public Guid Id { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
        public bool Borrowed { get; set; }
        public bool IsPublic { get; set; }
    }
}