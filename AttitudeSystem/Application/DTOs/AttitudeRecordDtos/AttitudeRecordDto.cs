namespace AttitudeSystem.Application.DTOs.AttitudeRecordDtos
{
    public class AttitudeRecordDto
    {
        public Guid Id { get; set; }
        public bool Attendance { get; set; }
        public string Activation { get; set; }
        public string SiteTasks { get; set; }
        public string Respect { get; set; }
        public string Assignment { get; set; }
        public string Feedback { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
