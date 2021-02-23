using System;

namespace bookshelf.DTO.Book.BookLogged
{
    public class ReviewLoggedDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        //why user is null
        public UserLoggedDTO User { get; set; }
        public int Votes { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}