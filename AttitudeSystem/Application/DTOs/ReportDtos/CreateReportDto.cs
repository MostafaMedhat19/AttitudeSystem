namespace AttitudeSystem.Application.DTOs.ReportDtos
{
    public class CreateReportDto
    {
        public string ReportName { get; set; }
        public IFormFile File { get; set; }
        public Guid StudentId { get; set; }
    }
}
