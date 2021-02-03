using System;

namespace bookshelf.Entities
{
    public class Admin : User
    {
        public Guid Id { get; set; }
        //TODO add connection to user
    }
}