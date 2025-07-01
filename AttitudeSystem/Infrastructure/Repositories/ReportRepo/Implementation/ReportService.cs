

namespace AttitudeSystem.Infrastructure.Repositories.ReportRepo.Implementation
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;
        private readonly IWhatsAppService _whatsAppService;
        private readonly IWebHostEnvironment _environment;

        public ReportService(
            AppDbContext context,
            IWhatsAppService whatsAppService,
            IWebHostEnvironment environment)
        {
            _context = context;
            _whatsAppService = whatsAppService;
            _environment = environment;
        }

        public async Task<Report> CreateReport(string reportName, IFormFile file, Guid studentId)
        {
          
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is required");

          
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "reports");

         
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

          
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

           
            var report = new Report
            {
                ReportName = reportName,
                StoredFileName = uniqueFileName,
                StudentId = studentId,
                IsAccepted = false
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task AcceptReport(Guid reportId)
        {
            var report = await _context.Reports
                .Include(r => r.Student)
                .FirstOrDefaultAsync(r => r.Id == reportId);

            if (report == null) throw new Exception("Report not found");

            report.IsAccepted = true;
            await _context.SaveChangesAsync();

           
            string message = $"The report '{report.ReportName}' regarding {report.Student.Name} has been approved by the principal.";
            await _whatsAppService.SendMessage(report.Student.GuardianPhoneNumber, message);
        }

        public async Task<List<Report>> GetAllReports()
        {
            return await _context.Reports
                .Include(r => r.Student)
                .ToListAsync();
        }

        public async Task<FileResult> DownloadReport(Guid reportId)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report == null) throw new Exception("Report not found");

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "reports");
            var filePath = Path.Combine(uploadsFolder, report.StoredFileName);

            if (!System.IO.File.Exists(filePath))
                throw new Exception("File not found");

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return new FileStreamResult(memory, GetContentType(filePath))
            {
                FileDownloadName = report.ReportName + Path.GetExtension(filePath)
            };
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
