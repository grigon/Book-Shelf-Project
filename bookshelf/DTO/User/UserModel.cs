using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshelf.DTO.User
{
    public class UserModel
    {
        public string UserName { get; set; }
        [Required, StringLength(50)]
        [Column(TypeName = "VARCHAR(50)")]
        public string Email { get; set; }
        [Required, StringLength(72)]
        [Column(TypeName = "VARCHAR(72)")]
        public string Password { get; set; }
        [Required, StringLength(72)]
        [Column(TypeName = "VARCHAR(72)")]
        public string City { get; set; }
        [Column(TypeName = "VARCHAR(200)")]
        public string PhotoPath { get; set; }
    }
}