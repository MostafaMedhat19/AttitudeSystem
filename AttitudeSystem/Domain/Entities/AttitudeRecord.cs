

namespace AttitudeSystem.Domain.Entities
{
    public class AttitudeRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool Attendance { get; set; }

        [Required(ErrorMessage = "Activation is required.")]
        [MaxLength(200, ErrorMessage = "Activation must be less than 200 characters.")]
        public string Activation { get; set; }

        [Required(ErrorMessage = "SiteTasks is required.")]
        [MaxLength(200, ErrorMessage = "SiteTasks must be less than 200 characters.")]
        public string SiteTasks { get; set; }

        [Required(ErrorMessage = "Respect is required.")]
        [MaxLength(200, ErrorMessage = "Respect must be less than 200 characters.")]
        public string Respect { get; set; }

        [Required(ErrorMessage = "Assignment is required.")]
        [MaxLength(200, ErrorMessage = "Assignment must be less than 200 characters.")]
        public string Assignment { get; set; }

        [Required(ErrorMessage = "Feedback is required.")]
        [MaxLength(500, ErrorMessage = "Feedback must be less than 500 characters.")]
        public string Feedback { get; set; }

        public DateTime RecordDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "StudentId is required.")]
        public Guid StudentId { get; set; }

        public Student Student { get; set; }
    }
}
