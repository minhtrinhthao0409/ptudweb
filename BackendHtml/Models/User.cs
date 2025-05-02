using System.ComponentModel.DataAnnotations;

namespace LoginRegisterExample.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
