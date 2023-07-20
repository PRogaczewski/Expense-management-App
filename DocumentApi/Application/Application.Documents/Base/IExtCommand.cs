namespace Application.Documents.Base
{
    public interface IExtCommand<T> : IBaseCommand
    {
        public T Result { get; protected set; }
    }
}
