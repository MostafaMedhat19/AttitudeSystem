

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [Authorize(Roles = "Manager")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateReport([FromForm] CreateReportDto request)
    {
        try
        {
            var report = await _reportService.CreateReport(request.ReportName, request.File, request.StudentId);
            return Ok(report);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

   
    [Authorize(Roles = "Principal")]
    [HttpPost("accept/{reportId}")]
    public async Task<IActionResult> AcceptReport(Guid reportId)
    {
        try
        {
            await _reportService.AcceptReport(reportId);
            return Ok(new { Message = "Report accepted successfully." });
        }
        catch (Exception ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }


    [Authorize]
    [HttpGet("download/{reportId}")]
    public async Task<IActionResult> DownloadReport(Guid reportId)
    {
        try
        {
            var fileResult = await _reportService.DownloadReport(reportId);
            return fileResult;
        }
        catch (Exception ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

  
    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllReports()
    {
        var reports = await _reportService.GetAllReports();
        return Ok(reports);
    }
}
