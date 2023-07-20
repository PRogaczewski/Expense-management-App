using Application.ExternalServices.IServices.Query;
using Application.ExternalServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReportController : Controller
    {
        [HttpGet("/PdfReport")]
        public async Task<IActionResult> PdfReport([FromServices] IDocumentServiceQuery<PdfResponse, PdfRequest> _documentService, int id)
        {
            try
            {
                var report = await _documentService.GetReport(id);

                return File(report.Document, "application/pdf", report.Filename);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
