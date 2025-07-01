

namespace AttitudeSystem.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        
        public string Name { get; set; }

      
        public string NationalId { get; set; }

       
        public string GuardianPhoneNumber { get; set; } 

       
        public string ClassName { get; set; }

        public Grade Grade { get; set; }

      
        public List<AttitudeRecord> Attitudes { get; set; } = new List<AttitudeRecord>();
    }
}
