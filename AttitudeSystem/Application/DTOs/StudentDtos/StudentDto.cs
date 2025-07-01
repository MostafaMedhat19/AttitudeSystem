using AttitudeSystem.Application.DTOs.AttitudeRecordDtos;
using AttitudeSystem.Domain.Enums;

namespace AttitudeSystem.Application.DTOs.StudentDtos
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string GuardianPhoneNumber { get; set; }
        public string ClassName { get; set; }
        public Grade Grade { get; set; }
        public List<AttitudeRecordDto> Attitudes { get; set; } = new List<AttitudeRecordDto>();
    }
}
