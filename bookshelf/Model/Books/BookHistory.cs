﻿using bookshelf.Model.Users;
using System;

namespace bookshelf.Model.Books
{
    public class BookHistory
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserBook { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}