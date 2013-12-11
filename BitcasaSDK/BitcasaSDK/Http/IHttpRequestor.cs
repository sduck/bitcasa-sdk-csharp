using System.Net.Http;
using System.Threading.Tasks;

namespace BitcasaSDK.Http
{
    public interface IHttpRequestor
    {
        Task<string> GetString(HttpMethod method, string url);
    }
}