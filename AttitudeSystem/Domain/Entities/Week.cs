

namespace AttitudeSystem.Domain.Entities
{
    public class Week
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Week number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Week number must be greater than 0.")]
        public int WeekNumber { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        public List<AttitudeRecord> Attitudes { get; set; } = new List<AttitudeRecord>();
    }
}
