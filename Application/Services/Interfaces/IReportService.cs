using Application.DTOs;

namespace Application.Services.Interfaces
{
    public interface IReportService
    {
        public Task<List<ReportDTO>> Filter(ReportFilter filter,Pagination pagination);
        public Task<int> Insert(ReportDTO dto, int districtId);
    }
}
