using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshelf.Model.Users
{
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(50)")]
        public Guid Id { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public User User { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public Role Role { get; set; }
    }
}