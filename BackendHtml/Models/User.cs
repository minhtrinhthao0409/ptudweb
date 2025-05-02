using System.ComponentModel.DataAnnotations;

namespace LoginRegisterExample.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();  // Sinh UUID dạng chuỗi
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Họ tên không được để trống.")]
        public string Fullname { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
