using AttitudeSystem.Domain.Enums;

namespace AttitudeSystem.Application.DTOs.RegisterDtos
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
