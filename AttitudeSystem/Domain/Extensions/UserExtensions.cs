

namespace AttitudeSystem.Domain.Extensions
{
    public static class UserExtensions
    {
        public static async Task<Principal> GetPrincipal(this User user, AppDbContext context)
        {
            return await context.Users.OfType<Principal>().FirstOrDefaultAsync(u => u.Id == user.Id);
        }

        public static async Task<VicePrincipal> GetVicePrincipal(this User user, AppDbContext context)
        {
            return await context.Users.OfType<VicePrincipal>().FirstOrDefaultAsync(u => u.Id == user.Id);
        }

        public static async Task<Manager> GetManager(this User user, AppDbContext context)
        {
            return await context.Users.OfType<Manager>().FirstOrDefaultAsync(u => u.Id == user.Id);
        }

        public static async Task<Teacher> GetTeacher(this User user, AppDbContext context)
        {
            return await context.Users.OfType<Teacher>().FirstOrDefaultAsync(u => u.Id == user.Id);
        }
    }
}
