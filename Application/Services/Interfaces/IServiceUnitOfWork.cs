namespace Application.Services.Interfaces
{
    public interface IServiceUnitOfWork
    {
        public IReportService ReportService {get;}
        public IDistrictService DistrictService {get;}
    }
}
