using bookshelf.Model.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshelf.Model.Chats
{
    public class ChatUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(40)")]
        public Guid Id { get; set; }
        [Column(TypeName = "VARCHAR(40)")]
        public Chat Chat { get; set; }
        [Column(TypeName = "VARCHAR(40)")]
        public User User { get; set; }
    }
}