using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services.Interfaces
{
    public class DistrictService(IRepositoryUnitOfWork repository, IMapper mapper) : IDistrictService
    {
        public async Task<List<DistrictDTO>> Get()
        {
            return mapper.Map<List<DistrictDTO>>(await repository.DistrictRepository.GetAllAsync());
        }
        public async Task Insert(DistrictDTO dto)
        {
            await repository.DistrictRepository.AddAsync(mapper.Map<District>(dto));
        }
    }
}