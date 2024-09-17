using AutoMapper;
using Cupcake.Application.Models.Adm.ProductModels;
using Cupcake.Application.Models.Store;
using Cupcake.Domain.Entities;

namespace Cupcake.Application.Mapping;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<CreateProductModel, Product>().ReverseMap();
        CreateMap<ProdutModel, Product>().ReverseMap();
    }
}
