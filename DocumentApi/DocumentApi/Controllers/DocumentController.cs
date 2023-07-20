using Application.Documents.Helpers;
using Application.Documents.IService;
using DocumentApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : Controller
    {
        private readonly IEnumerable<IDocumentService> _documentServices;

        public DocumentController(IEnumerable<IDocumentService> documentServices)
        {
            _documentServices = documentServices;
        }

        [HttpPost("/PdfDocument")]
        public async Task<ActionResult> PdfDocument(DocumentRequest req)
        {
            try
            {
                var result = await _documentServices.First(x=>x.DocumentFormat == DocumentFormat.Pdf.ToString()).CreateAndGetDocument(req.Text, req.Filename);

                //return File(result.Document, "application/pdf", result.Filename);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
