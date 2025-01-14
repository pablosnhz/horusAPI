using System.ComponentModel.DataAnnotations;

namespace HorusAPI.Models.Dto
{
    public class HorusDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(35)]
        public string Name { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
