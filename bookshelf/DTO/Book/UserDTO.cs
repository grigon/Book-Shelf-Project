using System;

namespace bookshelf.DTO.Book
{
    public class UserDTO
    {
    
        //public Guid Id { get; set; }
        public string UserName { get; set; }
        //public bool IsPublic { get; set; }
        public string PhotoPath { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}