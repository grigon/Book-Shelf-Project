using System;

namespace bookshelf.DTO.Book.BookLogged
{
    public class BookHistoryLoggedDTO
    {
        public Guid Id { get; set; }
        public Model.Users.User User { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}