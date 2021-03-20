using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshelf.Model.Chats
{
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "VARCHAR(40)")]
        public Guid ChatId { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; }
        public ICollection<ChatUser> ChatUsers { get; set; }
    }
}