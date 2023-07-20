using Application.Documents.Base;

namespace Application.Documents.Service.Pdf.Models
{
    public class GetPdfCommand : IExtCommand<byte[]>
    {
        public byte[] Result { get; set; }

        public void Handle()
        {

            
        }
    }
}
