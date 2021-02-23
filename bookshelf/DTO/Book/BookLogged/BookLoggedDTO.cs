using System;
using System.Collections.Generic;
using bookshelf.DTO.Book.Books;

namespace bookshelf.DTO.Book.BookLogged
{
    public class BookLoggedDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public AuthorLoggedDTO Author { get; set; }
        public string GenreGenreName { get; set; }
        public int Rating { get; set; }

        public ICollection<ReviewLoggedDTO> Reviews { get; set; }
        public ICollection<BookISBNDTO> BookISBNs { get; set; }
    }
}