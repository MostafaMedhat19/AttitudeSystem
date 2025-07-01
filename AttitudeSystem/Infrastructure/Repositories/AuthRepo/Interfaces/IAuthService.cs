

namespace AttitudeSystem.Infrastructure.Repositories.AuthRepo.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(string email, string password);
        Task<User> GetCurrentUser();
        Task<bool> IsInRole(UserRole role);
        Task<string> RegisterStudent(StudentRegisterDto dto);
        Task<string> Register(RegisterDto dto); 
    }
}
