using System;
using bookshelf.Model.Users;

namespace bookshelf.DTO.Book
{
    public class ReviewDTO
    {
        //For not logged/registered users
        public Guid Id { get; set; }
        public string Content { get; set; }
        public UserDTO User { get; set; }
        //public BookDTO Book { get; set; }
        public int Votes { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}