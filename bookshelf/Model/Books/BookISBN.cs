using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bookshelf.Model.Books
{
    public class BookISBN
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(40)")]
        public Guid Id { get; set; }
        [Column(TypeName = "VARCHAR(40)")]
        //public Guid BookId { get; set; }
        public Book Book { get; set; }
        [Column(TypeName = "VARCHAR(30)")]
        public string ISBN { get; set; }
    }
}