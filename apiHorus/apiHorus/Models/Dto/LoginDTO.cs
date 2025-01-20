using System.ComponentModel.DataAnnotations;

namespace apiHorus.Models.Dto
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
