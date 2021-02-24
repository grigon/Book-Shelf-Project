using System;
using System.Collections.Generic;

namespace bookshelf.DTO.Book.BookLogged
{
    public class UserBookDTO
    {
        public Guid Id { get; set; }
        public BookLoggedDTO Book { get; set; }
        public Model.Users.User User { get; set; }
        public bool Borrowed { get; set; }
        public bool IsPublic { get; set; }
        
       // public ICollection<BookHistoryLoggedDTO> BookHistory { get; set; }
    }
}