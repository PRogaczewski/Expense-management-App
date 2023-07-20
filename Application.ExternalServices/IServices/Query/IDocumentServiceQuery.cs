namespace Application.ExternalServices.IServices.Query
{
    public interface IDocumentServiceQuery<TReponse, TRequest>
    {
        Task<TReponse> GetReport(int id);
    }
}
