using AttitudeSystem.Application.DTOs.StudentDtos;
using AttitudeSystem.Application.DTOs.UserDtos;

namespace AttitudeSystem.Application.DTOs.PrincipalDtos
{
    public class PrincipalDto : UserDto
    {
        public List<string> Messages { get; set; } = new List<string>();
        public List<StudentDto> Students { get; set; } = new List<StudentDto>();
    }
}
