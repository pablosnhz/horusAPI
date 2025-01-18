using AutoMapper;
using HorusAPI.Models;
using HorusAPI.Models.Dto;

namespace HorusAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Horus, HorusDto>();
            CreateMap<HorusDto, Horus>();

            CreateMap<Horus, HorusCreateDto>().ReverseMap();
            CreateMap<Horus, HorusUpdateDto>().ReverseMap();

            CreateMap<NumeroHorus, NumeroHorusDto>().ReverseMap();
            CreateMap<NumeroHorus, HorusCreateDto>().ReverseMap();
            CreateMap<NumeroHorus, HorusUpdateDto>().ReverseMap();
        }
    }
}
