using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bookshelf.Model.Books;

namespace bookshelf.DTO.Book
{
    public class BookDTO
    {
        //This is DTO for main page to show to all not registered users
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
        
        //public string AuthorFirstName { get; set; }
        //public string AuthorLastName { get; set; }
        //public Guid GenreId { get; set; }
        //public string GenreName { get; set; }
        public GenreDTO Genre { get; set; }
        public string BookISBNISBN { get; set; }

        //public string UserUserName { get; set; }
        //public DateTime UserRegistrationDate { get; set; }
        
        public ICollection<Review> Reviews { get; set; }
    }
}