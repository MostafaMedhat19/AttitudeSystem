

namespace AttitudeSystem.Domain.Entities
{
    public class VicePrincipal : User
    {
        public List<string> Messages { get; set; } = new List<string>();
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
