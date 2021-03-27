﻿using System;
using System.Collections.Generic;
using bookshelf.Model.Books;
using bookshelf.Model.Chats;
using bookshelf.Model.NLog;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bookshelf.Context
{
    public class BaseDbContext : IdentityDbContext<User>
    {
        // private readonly string _connectionString;

        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<BookHistory> BookHistory { get; set; }
        public DbSet<Genre> Genres { get; set; }
       
        public DbSet<BookISBN> BookISBN { get; set; }

        public DbSet<LoggerEntityError> LoggerError { get; set; }
        public DbSet<LoggerEntityLow> LoggerLowState { get; set; }
    }
}