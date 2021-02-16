using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshelf.Model.Books
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(40)")]
        public Guid Id { get; set; }
        [Required, StringLength(80)]
        [Column(TypeName = "VARCHAR(80)")]
        public string Title { get; set; }
        [Column(TypeName = "VARCHAR(80)")]
        public Author Author { get; set; }
        [Column(TypeName = "VARCHAR(40)")]
        public Genre Genre { get; set; }
        public int Rating { get; set; }
    }
}