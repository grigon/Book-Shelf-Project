using System;
using bookshelf.Model.Users;

namespace bookshelf.Model.Books
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