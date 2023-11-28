using API.Models;
using API.ViewModels.ProductModel;
using AutoMapper;

namespace API
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile() 
        {
            CreateMap<Product, CreateProductModel>().ReverseMap();

        }    
    }
}
