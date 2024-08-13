using Application.DTOs;
using AutoMapper;
using Domain.Entities;
namespace Application.Mappers
{
    public class ReportMappingProfile : Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<Report, ReportDTO>().ReverseMap();
        }
    }
}