using System;
using System.Collections.Generic;
using bookshelf.Model.Books;

namespace bookshelf.DTO.Book
{
    public class BookDTO
    {
        //This is DTO for main page to show to all not registered users
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
       // public string GenreName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public GenreDTO GenreDto { get; set; }

        public ICollection<ReviewDTO> Reviews { get; set; }
        public ICollection<BookISBN> BookISBNs { get; set; }
    }
}