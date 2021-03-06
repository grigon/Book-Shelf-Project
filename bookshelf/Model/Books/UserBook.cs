﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bookshelf.Model.Users;

namespace bookshelf.Model.Books
{
    public class UserBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(30)")]
        public Guid Id { get; set; }
        [Column(TypeName = "VARCHAR(30)")]
        public Book Book { get; set; }
        [Column(TypeName = "VARCHAR(30)")]
        public User User { get; set; }
        public bool Borrowed { get; set; }
        public bool IsPublic { get; set; }
    }
}