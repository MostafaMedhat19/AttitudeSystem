
namespace AttitudeSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents();
            return Ok(students);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await _studentService.GetStudentById(id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

       
        [Authorize(Roles = "Manager,Principal")]
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] CreateStudentDto dto)
        {
            var student = await _studentService.AddStudent(dto);
            return Ok(student);
        }

       
        [Authorize(Roles = "Manager,Principal")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentDto dto)
        {
            var student = await _studentService.UpdateStudent(id, dto);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

       
        [Authorize(Roles = "Teacher")]
        [HttpPost("{studentId}/attitude")]
        public async Task<IActionResult> AddAttitudeRecord(Guid studentId, [FromBody] CreateAttitudeRecordDto dto)
        {
            var record = await _studentService.AddAttitudeRecord(studentId, dto);
            if (record == null)
                return NotFound(new { Message = "Student not found" });
            return Ok(record);
        }

       
        [Authorize]
        [HttpGet("{studentId}/attitude")]
        public async Task<IActionResult> GetStudentAttitudeRecords(Guid studentId)
        {
            var records = await _studentService.GetStudentAttitudeRecords(studentId);
            return Ok(records);
        }

     
        [Authorize(Roles = "Manager,Principal")]
        [HttpPost("week/create")]
        public async Task<IActionResult> CreateNewWeek()
        {
            await _studentService.CreateNewWeek();
            return Ok(new { Message = "New week created successfully." });
        }
    }
}
