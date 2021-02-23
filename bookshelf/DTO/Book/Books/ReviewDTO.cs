using System;

namespace bookshelf.DTO.Book
{
    public class ReviewDTO
    {
        //For not logged/registered users
        public Guid Id { get; set; }
        public string Content { get; set; }
        public UserDTO UserDto { get; set; }
        public int Votes { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}