using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshelf.Model.Users
{
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(40)")]
        public Guid Id { get; set; }
        [Column(TypeName = "VARCHAR(40)")]
        public User User { get; set; }
        [Column(TypeName = "VARCHAR(40)")]
        public Role Role { get; set; }
    }
}