using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshelf.Model.Users
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(40)")]
        public Guid Id { get; set; }
        [Required, StringLength(60)]
        [Column(TypeName = "VARCHAR(60)")]
        public string Name { get; set; }
    }
}