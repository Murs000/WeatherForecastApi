using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Application.Services.Interfaces
{
    public class ServiceUnitOfWork(IRepositoryUnitOfWork repository, IMapper mapper) :IServiceUnitOfWork
    {
        public IReportService ReportService => new ReportService(repository,mapper);
        public IDistrictService DistrictService => new DistrictService(repository,mapper);
    }
}
