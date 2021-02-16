using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshelf.Model.Users
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(40)")]
        public Guid Id { get; set; }
        [Required, StringLength(60)]
        [Column(TypeName = "VARCHAR(60)")]
        public string UserName { get; set; }
        [Required, StringLength(50)]
        [Column(TypeName = "VARCHAR(50)")]
        public string Email { get; set; }
        [Required, StringLength(72)]
        [Column(TypeName = "VARCHAR(72)")]
        public string Password { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "VARCHAR(70)")]
        public string City { get; set; }
        [Column(TypeName = "VARCHAR(200)")]
        public string PhotoPath { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}