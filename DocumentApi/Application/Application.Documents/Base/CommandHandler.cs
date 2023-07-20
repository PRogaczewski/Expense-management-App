namespace Application.Documents.Base
{
    public abstract class CommandHandler
    {
        protected void Handle(IBaseCommand cmd)
        {
            cmd.Handle();
        }

        protected T Handle<T>(IExtCommand<T> cmd)
        {
            cmd.Handle();

            return cmd.Result;
        }
    }
}
