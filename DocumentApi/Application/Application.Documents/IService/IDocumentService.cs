namespace Application.Documents.IService
{
    public interface IDocumentService
    {
        public string DocumentFormat { get; set; }

        Task<DocumentResponse> CreateAndGetDocument(string text, string filename);
    }
}
