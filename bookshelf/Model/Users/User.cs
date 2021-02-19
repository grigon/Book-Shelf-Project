using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace bookshelf.Model.Users
{
    public class User : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(36)")]
        public Guid Id { get; set; }
        [Required, StringLength(50)]
        [Column(TypeName = "VARCHAR(50)")]
        // public string UserName { get; set; }
        // [Required, StringLength(50)]
        // [Column(TypeName = "VARCHAR(50)")]
        // [PersonalData]
        // public string Email { get; set; }
        // [Required, StringLength(72)]
        // [Column(TypeName = "VARCHAR(72)")]
        // public string Password { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "VARCHAR(70)")]
        public string City { get; set; }
        [Column(TypeName = "VARCHAR(200)")]
        public string PhotoPath { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}