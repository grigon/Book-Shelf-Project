﻿using System;

namespace bookshelf.Model.Books
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public int Rating { get; set; }
    }
}