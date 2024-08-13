using Application.DTOs;
using AutoMapper;
using Domain.Entities;
namespace Application.Mappers
{
    public class DistrictMappingProfile : Profile
    {
        public DistrictMappingProfile()
        {
            CreateMap<DistrictDTO, District>().ReverseMap();
        }
    }
}
