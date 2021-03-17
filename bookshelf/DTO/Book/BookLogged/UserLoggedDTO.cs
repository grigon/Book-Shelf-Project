﻿using System;
using bookshelf.Model.Users;

namespace bookshelf.DTO.Book.BookLogged
{
    public class UserLoggedDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public string PhotoPath { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}