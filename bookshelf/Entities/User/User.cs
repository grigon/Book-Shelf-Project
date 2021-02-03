using System;

namespace bookshelf.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public string City { get; set; }
        public string PhotoPath { get; set; }
        //public Role Role { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}