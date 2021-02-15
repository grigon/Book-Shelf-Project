using bookshelf.Model.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshelf.Model.Chats
{
    public class ChatMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(30)")]
        public Guid Id { get; set; }
        public DateTime MessageDate { get; set; }
        public string Message { get; set; }
        [Column(TypeName = "VARCHAR(30)")]
        public User MessageAuthor { get; set; }
        [Column(TypeName = "VARCHAR(30)")]
        public Chat Chat { get; set; }
    }
}