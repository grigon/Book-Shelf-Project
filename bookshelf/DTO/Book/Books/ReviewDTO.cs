using System;

namespace bookshelf.DTO.Book
{
    public class ReviewDTO
    {
        //For not logged/registered users
        public Guid Id { get; set; }
        public string Content { get; set; }
        /*public string UserUserName { get; set; }
        public string UserPhotoPath { get; set; }
        public DateTime UserRegistrationDate { get; set; }*/
        public int Votes { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}