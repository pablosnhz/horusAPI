using System.ComponentModel.DataAnnotations;

namespace HorusAPI.Models.Dto
{
    public class NumeroHorusDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int HorusId { get; set; }

        public string DetalleEspecial { get; set; }
    }
}
