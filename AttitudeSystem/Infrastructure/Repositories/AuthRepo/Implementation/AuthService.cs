
namespace AttitudeSystem.Infrastructure.Repositories.AuthRepo.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();
            _mapper = mapper;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !VerifyHashedPassword(user, password))
                throw new UnauthorizedAccessException("Invalid credentials");

            return GenerateJwtToken(user);
        }
        public async Task<string> Register(RegisterDto dto)
        {
            
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null)
                throw new InvalidOperationException("Email is already registered.");
            var user = _mapper.Map<User>(dto);

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User registered successfully.";
        }

        public async Task<string> RegisterStudent(StudentRegisterDto dto)
        {
           
            var validationContext = new ValidationContext(dto);
            Validator.ValidateObject(dto, validationContext, validateAllProperties: true);

          
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.NationalId == dto.NationalId);

            if (existingStudent != null)
                throw new InvalidOperationException("Student with this National ID already exists.");

           
            var student = _mapper.Map<Student>(dto);

        
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return "Student registered successfully.";
        }

        public async Task<User?> GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return null;

            return await _context.Users.FindAsync(Guid.Parse(userId));
        }

        public async Task<bool> IsInRole(UserRole role)
        {
            var user = await GetCurrentUser();
            return user?.Role == role;
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
      
                new Claim(ClaimTypes.Role, user.Role.ToString()),
    
                new Claim("role", user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private bool VerifyHashedPassword(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
