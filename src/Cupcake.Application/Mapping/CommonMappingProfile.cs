using AutoMapper;
using Cupcake.Application.Models.Common;
using Cupcake.Domain.Entities;

namespace Cupcake.Application.Mapping;

public class CommonMappingProfile : Profile
{
    public CommonMappingProfile()
    {
        CreateMap<BaseEntityModel, BaseEntity>().ReverseMap();
        CreateMap<UserModel, User>().ReverseMap();
    }
}
