using HorusAPI.Models.Dto;

namespace HorusAPI.Datos
{
    public static class HorusStore
    {
        public static List<HorusDto> horusList = new List<HorusDto>
        {
            new HorusDto {Id = 1, Name = "Nuevo dispositivo"},
            new HorusDto {Id = 2, Name = "Dispositivo alta gama"}
        };
    }
}
