using System;
using System.Collections.Generic;
using bookshelf.DTO.Book.Books;
using bookshelf.Model.Books;

namespace bookshelf.DTO.Book.BookLogged
{
    public class UserBookDTO
    {
        public Guid Id { get; set; }
        public Model.Books.Book Book { get; set; }
        public Model.Users.User User { get; set; }
        public bool Borrowed { get; set; }
        public bool IsPublic { get; set; }
        
        public ICollection<BookHistory> BookHistories { get; set; }
        public ICollection<ReviewLoggedDTO> Reviews { get; set; }
        public ICollection<BookISBNDTO> BookISBNs { get; set; }
    }
}