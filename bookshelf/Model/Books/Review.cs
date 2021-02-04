using bookshelf.Model.Users;
using System;


namespace bookshelf.Model.Books
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public int Votes { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}