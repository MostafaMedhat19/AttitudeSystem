

namespace AttitudeSystem.Domain.Entities
{
    public class Report
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Report name is required.")]
        [MaxLength(200, ErrorMessage = "Report name must be less than 200 characters.")]
        public string ReportName { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "Stored file name is required.")]
        [MaxLength(300, ErrorMessage = "Stored file name must be less than 300 characters.")]
        public string StoredFileName { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "StudentId is required.")]
        public Guid StudentId { get; set; }

        public Student Student { get; set; }
    }
}
