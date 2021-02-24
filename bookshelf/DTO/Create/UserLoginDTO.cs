using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshelf.DTO.Create
{
    public class UserLoginDTO
    {
        [Required, StringLength(50)]
        [Column(TypeName = "VARCHAR(50)")]
        public string Email { get; set; }
        [Required, StringLength(72)]
        [Column(TypeName = "VARCHAR(72)")]
        public string Password { get; set; }
    }
}