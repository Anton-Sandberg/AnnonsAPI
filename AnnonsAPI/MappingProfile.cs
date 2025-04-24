using AnnonsAPI.DTOs;
using AnnonsAPI.Models;
using AutoMapper;

namespace AnnonsAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdCreateDto, Ad>()
                .ForMember(dest => dest.CreatedAt,
                           opt => opt.MapFrom(createdAt => DateTime.UtcNow));

            CreateMap<Ad, AdReadDto>();

            CreateMap<AdUpdateDto, Ad>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
