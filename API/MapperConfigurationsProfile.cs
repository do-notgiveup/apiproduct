using API.Models;
using API.ViewModels.CategoryModel;
using API.ViewModels.ProductModel;
using AutoMapper;

namespace API
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<Product, CreateProductModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>()
                .ForMember(des => des.CateName, src => src.MapFrom(x => x.Cate.CateName))
                .ReverseMap();
            CreateMap<Category, CreateCategoryModel>().ReverseMap();

        }
    }
}
