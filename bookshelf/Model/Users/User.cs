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
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "VARCHAR(70)")]
        public string City { get; set; }
        [Column(TypeName = "VARCHAR(200)")]
        public string PhotoPath { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}