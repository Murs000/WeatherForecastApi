using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Application.Services.Implementations
{
    public class ReportService(IRepositoryUnitOfWork repository, IMapper mapper) : IReportService
    {
        public async Task<List<ReportDTO>> Filter(ReportFilter filter,Pagination pagination)
        {
            var query = await repository.ReportRepository.GetAllAsync();

            if (filter.WeatherId.HasValue)
                query = query.Where(r => r.WeatherId == filter.WeatherId.Value);

            if (!string.IsNullOrEmpty(filter.Main))
                query = query.Where(r => r.Main == filter.Main);

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(r => r.Description == filter.Description);

            if (filter.TempMin.HasValue)
                query = query.Where(r => r.Temp >= filter.TempMin.Value);

            if (filter.TempMax.HasValue)
                query = query.Where(r => r.Temp <= filter.TempMax.Value);

            if (filter.FeelsLikeMin.HasValue)
                query = query.Where(r => r.FeelsLike >= filter.FeelsLikeMin.Value);

            if (filter.FeelsLikeMax.HasValue)
                query = query.Where(r => r.FeelsLike <= filter.FeelsLikeMax.Value);

            if (filter.PressureMin.HasValue)
                query = query.Where(r => r.Pressure >= filter.PressureMin.Value);

            if (filter.PressureMax.HasValue)
                query = query.Where(r => r.Pressure <= filter.PressureMax.Value);

            if (filter.HumidityMin.HasValue)
                query = query.Where(r => r.Humidity >= filter.HumidityMin.Value);

            if (filter.HumidityMax.HasValue)
                query = query.Where(r => r.Humidity <= filter.HumidityMax.Value);

            if (filter.WindSpeedMin.HasValue)
                query = query.Where(r => r.WindSpeed >= filter.WindSpeedMin.Value);

            if (filter.WindSpeedMax.HasValue)
                query = query.Where(r => r.WindSpeed <= filter.WindSpeedMax.Value);

            if (filter.WindDegreeMin.HasValue)
                query = query.Where(r => r.WindDegree >= filter.WindDegreeMin.Value);

            if (filter.WindDegreeMax.HasValue)
                query = query.Where(r => r.WindDegree <= filter.WindDegreeMax.Value);

            if (filter.CloudsMin.HasValue)
                query = query.Where(r => r.Clouds >= filter.CloudsMin.Value);

            if (filter.CloudsMax.HasValue)
                query = query.Where(r => r.Clouds <= filter.CloudsMax.Value);

            if (filter.DisctrictId.HasValue)
                query = query.Where(r => r.DisctrictId == filter.DisctrictId.Value);

            if (filter.DateTimeStart.HasValue)
                query = query.Where(r => r.DateTime >= filter.DateTimeStart.Value);

            if (filter.DateTimeEnd.HasValue)
                query = query.Where(r => r.DateTime <= filter.DateTimeEnd.Value);

            var paginatedReports = query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();

            return mapper.Map<List<ReportDTO>>(paginatedReports);
        }
        public async Task<int> Insert(ReportDTO dto, int districtId)
        {
            var entity = await repository.ReportRepository.GetByIdAsync(dto.Id);
            entity.DisctrictId = districtId;
            entity.SetDate();
            await repository.ReportRepository.AddAsync(entity);
            return entity.Id;
        }
    }
}
