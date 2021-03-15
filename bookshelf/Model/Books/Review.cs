using bookshelf.Model.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace bookshelf.Model.Books
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(40)")]
        public Guid Id { get; set; }
        public string Content { get; set; }
       // [ForeignKey("UserId")]
        [Column(TypeName = "VARCHAR(40)")]
        public User User { get; set; }
       // [ForeignKey("BookId")]
        [Column(TypeName = "VARCHAR(40)")]
        public Book Book { get; set; }
        public int Votes { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}