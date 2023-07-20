namespace Application.ExternalServices.Models
{
    public class PdfResponse
    {
        public byte[] Document { get; set; }

        public string Filename { get; set; }
    }
}
