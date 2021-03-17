using System;
using bookshelf.Model.Users;

namespace bookshelf.DTO.Book.BookLogged
{
    public class ReviewLoggedDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public UserLoggedDTO User { get; set; }
        public int Votes { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}