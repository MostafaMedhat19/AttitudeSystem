using AttitudeSystem.Application.DTOs.StudentDtos;

namespace AttitudeSystem.Application.DTOs.ReportDtos
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public string ReportName { get; set; }
        public string StoredFileName { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime CreatedDate { get; set; }
        public StudentDto Student { get; set; }
    }
}
