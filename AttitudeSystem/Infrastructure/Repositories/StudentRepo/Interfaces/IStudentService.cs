

namespace AttitudeSystem.Infrastructure.Repositories.StudentRepo.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> GetStudentById(Guid id);
        Task<List<StudentDto>> GetAllStudents();
        Task<StudentDto> AddStudent(CreateStudentDto studentDto);
        Task<StudentDto> UpdateStudent(Guid id, UpdateStudentDto studentDto);
        Task<AttitudeRecordDto> AddAttitudeRecord(Guid studentId, CreateAttitudeRecordDto recordDto);
        Task<List<AttitudeRecordDto>> GetStudentAttitudeRecords(Guid studentId);
        Task CreateNewWeek();
       
    }
}
