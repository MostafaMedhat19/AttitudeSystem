using AttitudeSystem.Domain.Enums;

namespace AttitudeSystem.Application.DTOs.StudentDtos
{
    public class CreateStudentDto
    {
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string GuardianPhoneNumber { get; set; }
        public string ClassName { get; set; }
        public Grade Grade { get; set; }

    }
}
