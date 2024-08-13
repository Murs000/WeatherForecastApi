using Application.DTOs;

namespace Application.Services.Interfaces
{
    public interface IDistrictService
    {
        public Task<List<DistrictDTO>> Get();
        public Task Insert(DistrictDTO dto);
    }
}