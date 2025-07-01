using AttitudeSystem.Domain.Enums;

namespace AttitudeSystem.Application.DTOs.UserDtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
