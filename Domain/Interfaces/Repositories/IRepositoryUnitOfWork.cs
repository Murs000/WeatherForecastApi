using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryUnitOfWork
    {
        IRepository<District> DistrictRepository { get; }
        IRepository<Report> ReportRepository { get; }
    }
}