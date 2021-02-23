using System;
using System.Collections.Generic;
using bookshelf.Model.Books;

namespace bookshelf.DTO.Book.BookLogged
{
    public class BookLoggedDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public AuthorLoggedDTO Author { get; set; }
        public GenreLoggedDTO Genre { get; set; }
        public int Rating { get; set; }

        public ICollection<ReviewLoggedDTO> Reviews { get; set; }
        public ICollection<BookISBNLoggedDTO> BookISBNs { get; set; }
    }
}