using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bookshelf.Model.Users;

namespace bookshelf.Model.Books
{
    public class UserBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(40)")]
        public Guid Id { get; set; }
        [Column(TypeName = "VARCHAR(40)")]
        public Book Book { get; set; }
        [Column(TypeName = "VARCHAR(40)")]
        public User User { get; set; }
        public bool Borrowed { get; set; }
        public bool IsPublic { get; set; }
        
        public ICollection<BookHistory> BookHistories { get; set; }
    }
}