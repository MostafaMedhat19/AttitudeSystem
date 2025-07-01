using AttitudeSystem.Application.DTOs.StudentDtos;
using AttitudeSystem.Application.DTOs.UserDtos;

namespace AttitudeSystem.Application.DTOs.ManagerDtos
{
    public class ManagerDto : UserDto
    {
        public List<string> Messages { get; set; } = new List<string>();
        public List<StudentDto> ProblemStudents { get; set; } = new List<StudentDto>();
    }
}
