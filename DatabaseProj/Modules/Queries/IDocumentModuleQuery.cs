namespace Domain.Modules.Queries
{
    public interface IDocumentModuleQuery<T, TRequest>
    {
        Task<T> GetReport(TRequest model);
    }
}
