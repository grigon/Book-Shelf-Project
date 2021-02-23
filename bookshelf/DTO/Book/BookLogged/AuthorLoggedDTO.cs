using System;

namespace bookshelf.DTO.Book.BookLogged
{
    public class AuthorLoggedDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}