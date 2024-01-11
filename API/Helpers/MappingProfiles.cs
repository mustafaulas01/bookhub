
using API.Dto;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
             CreateMap<Product,ProductToReturnDto>()
            .ForMember(p=>p.PictureUrl,o=>o.MapFrom<ProductUrlResolver>())
            .ForMember(p=>p.Publisher,m=>m.MapFrom(p=>p.Publisher.Name))
            .ForMember(p=>p.Category,o=>o.MapFrom(d=>d.Category.Name));
            
        }
        
    }
}