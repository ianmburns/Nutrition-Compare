using System;

namespace Service4u2.Common
{
    public interface IAsyncService<TResultType>
    {
        event EventHandler<EventArgs<TResultType>> GotResult;

        event EventHandler<EventArgs<Exception>> GotError;
    }
}
