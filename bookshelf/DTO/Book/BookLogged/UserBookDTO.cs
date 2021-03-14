using System;
using System.Collections.Generic;
using bookshelf.DTO.Book.Books;
using bookshelf.Model.Books;

namespace bookshelf.DTO.Book.BookLogged
{
    public class UserBookDTO
    {
        public Guid Id { get; set; }
        public BookLoggedDTO Book { get; set; }
        public bool Borrowed { get; set; }
        public bool IsPublic { get; set; }
        
        public ICollection<BookHistory> BookHistories { get; set; }
    }
}