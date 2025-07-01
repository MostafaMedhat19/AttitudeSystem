

namespace AttitudeSystem.Infrastructure.Middleware
{
    public class UserAssociationMiddleware
    {
        private readonly RequestDelegate _next;

        public UserAssociationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService, AppDbContext dbContext)
        {
            // Skip middleware for authentication endpoints
            if (context.Request.Path.StartsWithSegments("/api/auth"))
            {
                await _next(context);
                return;
            }

            var user = await authService.GetCurrentUser();
            if (user == null)
            {
                await _next(context);
                return;
            }

         
            context.Items["CurrentUser"] = user;

           
            if (context.Request.RouteValues.TryGetValue("studentId", out var studentIdObj))
            {
                if (Guid.TryParse(studentIdObj?.ToString(), out var studentId))
                {
                    var isAssociated = await IsUserAssociatedWithStudent(user, studentId, dbContext);
                    if (!isAssociated)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("You are not associated with this student");
                        return;
                    }
                }
            }

            await _next(context);
        }

        private async Task<bool> IsUserAssociatedWithStudent(User user, Guid studentId, AppDbContext context)
        {
            switch (user.Role)
            {
                case UserRole.Principal:
                    return await context.Principals
                        .AnyAsync(p => p.Id == user.Id && p.Students.Any(s => s.Id == studentId));
                case UserRole.VicePrincipal:
                    return await context.VicePrincipals
                        .AnyAsync(vp => vp.Id == user.Id && vp.Students.Any(s => s.Id == studentId));
                case UserRole.Manager:
                    return await context.Managers
                        .AnyAsync(m => m.Id == user.Id && m.ProblemStudents.Any(s => s.Id == studentId));
                case UserRole.Teacher:
                    return await context.Teachers
                        .AnyAsync(t => t.Id == user.Id && t.Students.Any(s => s.Id == studentId));
                default:
                    return false;
            }
        }
    }

}
