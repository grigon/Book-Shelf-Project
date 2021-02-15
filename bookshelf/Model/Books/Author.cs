using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshelf.Model.Books
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(30)")]
        public Guid Id { get; set; }
        [Required, StringLength(80)]
        [Column(TypeName = "VARCHAR(80)")]
        public string FirstName { get; set; }
        [Required, StringLength(80)]
        [Column(TypeName = "VARCHAR(80)")]
        public string LastName { get; set; }
    }
}