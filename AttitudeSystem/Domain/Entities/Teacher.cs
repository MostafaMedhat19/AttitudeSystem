

namespace AttitudeSystem.Domain.Entities
{
    public class Teacher : User
    {
        //public Guid Id { get; set; } = Guid.NewGuid();

        //[Required(ErrorMessage = "Name is required.")]
        //[MaxLength(100, ErrorMessage = "Name must be less than 100 characters.")]
        //public string Name { get; set; }

        //[Required(ErrorMessage = "Email is required.")]
        //[EmailAddress(ErrorMessage = "Invalid email address.")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        //[MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        //[MaxLength(100, ErrorMessage = "Password must be less than 100 characters.")]
        //public string Password { get; set; }

        public List<string> Messages { get; set; } = new List<string>();

        public List<Student> Students { get; set; } = new List<Student>();
    }
}
