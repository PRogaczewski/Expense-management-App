using Application.Documents.Base;
using Application.Documents.IService;
using Application.Documents.Service.Pdf.Models;

namespace Application.Documents.Service.Pdf
{
    public class PdfService : CommandHandler, IDocumentService
    {
        public string DocumentFormat { get; set; } = "Pdf";

        public async Task<DocumentResponse> CreateAndGetDocument(string text, string filename)
        {
            if(string.IsNullOrEmpty(text))
            {
                throw new Exception("No contenet providded");
            }

            if(string.IsNullOrEmpty(filename))
            {
                filename = "NewDocument";
            }

            var cmd = CreatePdfCommand.CreateCommand(text, filename);

            var result = await Task.Run(() =>
            {
                return Handle(cmd);
            });   

            return result;
        }
    }
}
