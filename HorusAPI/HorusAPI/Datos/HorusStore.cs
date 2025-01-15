using HorusAPI.Models.Dto;

namespace HorusAPI.Datos
{
    public static class HorusStore
    {
        public static List<HorusDto> horusList = new List<HorusDto>
        {
            new HorusDto {Id = 1, Name = "Nuevo dispositivo",
                Description = "Celular con las ultimas actualizaciones"},
            new HorusDto {Id = 2, Name = "Dispositivo alta gama",
                Description = "Lo ultimo en celulares"}
        };
    }
}
