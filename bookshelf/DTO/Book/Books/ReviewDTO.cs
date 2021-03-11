using System;

namespace bookshelf.DTO.Book
{
    public class ReviewDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public UserDTO User { get; set; }
        public int Votes { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}