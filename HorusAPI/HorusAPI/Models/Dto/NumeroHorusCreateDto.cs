using System.ComponentModel.DataAnnotations;

namespace HorusAPI.Models.Dto
{
    public class NumeroHorusUpdateDto
    {
        [Required]
        public int HorusNo { get; set; }
        [Required]
        public int HorusId { get; set; }

        public string DetalleEspecial { get; set; }
    }
}
