using System;
using bookshelf.Model.Users;

namespace bookshelf.DTO.Book
{
    public class ReviewDTO
    {
        //For not logged/registered users
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        //public BookDTO Book { get; set; }
        public int Votes { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}