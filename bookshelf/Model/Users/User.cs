using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace bookshelf.Model.Users
{
    public class User : IdentityUser
    {
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public bool RememberMe { get; set; }
        [Column(TypeName = "VARCHAR(70)")]
        public string City { get; set; }
        [Column(TypeName = "VARCHAR(200)")]
        public string PhotoPath { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}