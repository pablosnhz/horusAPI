using System.ComponentModel.DataAnnotations;

namespace apiHorus.Models.Dto
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
