using AutoMapper;
using USASales.Models;
using USASales.Models.Dto;

namespace USASales
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
