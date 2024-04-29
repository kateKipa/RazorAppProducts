using WebRazorAppProducts.DTO;
using WebRazorAppProducts.Models;
using AutoMapper;

namespace WebRazorAppProducts.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ProductInsertDTO, Product>().ReverseMap();
            CreateMap<ProductUpdateDTO, Product>().ReverseMap();
            CreateMap<ProductReadOnlyDTO, Product>().ReverseMap();
        }
    }
}
