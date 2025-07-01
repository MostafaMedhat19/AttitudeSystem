

namespace AttitudeSystem.Infrastructure.Repositories.ReportRepo.Interfaces
{
    public interface IReportService
    {
        Task<Report> CreateReport(string reportName, IFormFile file, Guid studentId);
        Task AcceptReport(Guid reportId);
        Task<List<Report>> GetAllReports();
        Task<FileResult> DownloadReport(Guid reportId);
    }
}
