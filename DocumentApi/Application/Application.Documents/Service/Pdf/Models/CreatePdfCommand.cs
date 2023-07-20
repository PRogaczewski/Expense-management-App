using Application.Documents.Base;
using Application.Exceptions;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace Application.Documents.Service.Pdf.Models
{
    public class CreatePdfCommand : IExtCommand<DocumentResponse>
    {
        private CreatePdfCommand(string text, string filename) 
        {
            Text = text;
            Filename = filename;
            Result = new DocumentResponse();
        }

        public DocumentResponse Result { get; set; }

        private string Text { get; set; }

        private string Filename { get; set; }

        public void Handle()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            Result.Filename = Filename + "__" + $"{DateTime.Now}" + ".pdf";

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics graphics = XGraphics.FromPdfPage(page);

            XRect textRect = new XRect(50, 50, page.Width - 100, page.Height - 100);
            XTextFormatter textFormatter = new XTextFormatter(graphics);

            //string analysis = "Analiza wyników:\n\n" +
            //                  "Ogólny wynik: 80%\n" +
            //                  "Wynik sekcji A: 75%\n" +
            //                  "Wynik sekcji B: 85%\n" +
            //                  "Wynik sekcji C: 90%\n\n" +
            //                  "Podsumowanie: Uzyskano dobre wyniki we wszystkich sekcjach.";

            XFont font = new XFont("Arial", 14, XFontStyle.Bold);
            XStringFormat format = new XStringFormat()
            {
                Alignment = XStringAlignment.Near,
                LineAlignment = XLineAlignment.Near
            };

            textFormatter.DrawString(Text, font, XBrushes.Black, textRect, format);

            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                Result.Document = ms.ToArray();
            }

            if(Result is null || Result.Document.Length == 0)
            {
                throw new DocumentNotCreatedException("Pdf document not created", 400);
            }    
        }

        public static CreatePdfCommand CreateCommand(string text, string filename)
        {
            return new(text, filename);
        }
    }
}
