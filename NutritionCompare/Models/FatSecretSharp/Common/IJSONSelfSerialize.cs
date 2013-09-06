namespace FatSecretSharp.Services.Common.Service4u2Lib.Json
{
    public interface IJSONSelfSerialize<TResult>
    {
        TResult SelfSerialize(string json);
    }
}
