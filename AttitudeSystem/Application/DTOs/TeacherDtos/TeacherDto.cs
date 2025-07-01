using AttitudeSystem.Application.DTOs.StudentDtos;
using AttitudeSystem.Application.DTOs.UserDtos;
using AttitudeSystem.Domain.Enums;

namespace AttitudeSystem.Application.DTOs.TeacherDtos
{
    public class TeacherDto : UserDto
    {
        public string TeacherSubject { get; set; }
        public List<StudentDto> Students { get; set; } = new List<StudentDto>();
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
