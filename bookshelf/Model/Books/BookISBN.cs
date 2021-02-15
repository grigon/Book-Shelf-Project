using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace bookshelf.Model.Books
{
    public class BookISBN
    {
        public Guid Id { get; set; }
        public Book Book { get; set; }
        public string ISBNLight { get; set; }
        public string ISBNHard { get; set; }
    }
}