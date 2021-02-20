using System;
using System.ComponentModel.DataAnnotations;
using bookshelf.Model.Books;

namespace bookshelf.DTO.Book
{
    public class BookDTO
    {
        //This is DTO for main page to show to all not registered users
        public Guid Id { get; set; }
        [Required, StringLength(80)]
        public string Title { get; set; }
        public int Rating { get; set; }
        
        [Required, StringLength(80)]
        public string AuthorFirstName { get; set; }
        [Required, StringLength(80)]
        public string AuthorLastName { get; set; }
        
        [Required, StringLength(60)]
        public string GenreName { get; set; }
        
        public string BookISBNISBN { get; set; }
        
        public string ReviewContent { get; set; }
        public int ReviewVotes { get; set; }
        public DateTime ReviewReviewDate { get; set; }
        
        public string UserUserName { get; set; }
        public DateTime UserRegistrationDate { get; set; }
    }
}