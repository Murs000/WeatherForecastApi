using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class RepositoryUnitOfWork(AppDbContext context) : IRepositoryUnitOfWork
    {
        public IRepository<District> DistrictRepository => new RepositoryBase<District>(context);
        public IRepository<Report> ReportRepository => new RepositoryBase<Report>(context);
    }
}