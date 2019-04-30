namespace SIS.HTTP.Headers.Contracts
{
    public interface IHttpHeaderCollection
    {
        void Add(HttpHeader httpHeader);

        bool ContainsHeader(string key);

        HttpHeader GetHeader(string key);
    }
}
