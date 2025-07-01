using AttitudeSystem.Application.DTOs.AttitudeRecordDtos;
using AttitudeSystem.Application.DTOs.StudentDtos;
using AttitudeSystem.Domain.Entities;
using AttitudeSystem.Infrastructure.Data;
using AttitudeSystem.Infrastructure.Repositories.StudentRepo.Interfaces;
using AttitudeSystem.Infrastructure.Repositories.WhatsAppRepo.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttitudeSystem.Infrastructure.Repositories.StudentRepo.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWhatsAppService _whatsAppService;
        private readonly IConfiguration _configuration;

        public StudentService(
            AppDbContext context,
            IMapper mapper,
            IWhatsAppService whatsAppService,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _whatsAppService = whatsAppService;
            _configuration = configuration;
        }

        public async Task<StudentDto> GetStudentById(Guid id)
        {
            var student = await _context.Students
                .Include(s => s.Attitudes)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return null;

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<List<StudentDto>> GetAllStudents()
        {
            var students = await _context.Students
                .Include(s => s.Attitudes)
                .ToListAsync();

            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<StudentDto> AddStudent(CreateStudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> UpdateStudent(Guid id, UpdateStudentDto studentDto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return null;

            _mapper.Map(studentDto, student);
            await _context.SaveChangesAsync();
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<AttitudeRecordDto> AddAttitudeRecord(Guid studentId, CreateAttitudeRecordDto recordDto)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
                return null;

            var record = _mapper.Map<AttitudeRecord>(recordDto);
            record.StudentId = studentId;
            _context.AttitudeRecords.Add(record);
            await _context.SaveChangesAsync();

          
            await CheckMistakeLimit(student);

            return _mapper.Map<AttitudeRecordDto>(record);
        }

        public async Task<List<AttitudeRecordDto>> GetStudentAttitudeRecords(Guid studentId)
        {
            var records = await _context.AttitudeRecords
                .Where(a => a.StudentId == studentId)
                .ToListAsync();

            return _mapper.Map<List<AttitudeRecordDto>>(records);
        }

        public async Task CreateNewWeek()
        {
            var lastWeek = await _context.Weeks.OrderByDescending(w => w.WeekNumber).FirstOrDefaultAsync();
            int newWeekNumber = lastWeek == null ? 1 : lastWeek.WeekNumber + 1;

            var newWeek = new Week
            {
                WeekNumber = newWeekNumber,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(7)
            };

            _context.Weeks.Add(newWeek);
            await _context.SaveChangesAsync();
        }

        private async Task CheckMistakeLimit(Student student)
        {
            int mistakeLimit = _configuration.GetValue<int>("BehaviorSettings:MistakeLimit");
            var recentMistakes = student.Attitudes
                .Count(a => !a.Attendance ||
                            a.Activation == "Poor" ||
                            a.SiteTasks == "Incomplete" ||
                            a.Respect == "Disrespectful");

            if (recentMistakes >= mistakeLimit)
            {
                
                var manager = await _context.Managers.FirstOrDefaultAsync();
                if (manager != null)
                {
                    manager.Messages.Add($"Student {student.Name} from {student.ClassName} has exceeded the mistake limit.");
                    manager.ProblemStudents.Add(student);
                    await _context.SaveChangesAsync();
                }

               
                string message = $"{student.Name} has made {recentMistakes} mistakes today. A report may be created if this behavior repeats.";
                await _whatsAppService.SendMessage(student.GuardianPhoneNumber, message);
            }
        }

       
    }
}
