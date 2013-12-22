using System.Net.Http;
using System.Threading.Tasks;

namespace BitcasaSdk.Http
{
    public class HttpRequestor : IHttpRequestor
    {
        public async Task<string> GetString(HttpMethod method, string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(method, url);
            var responseMsg = await client.SendAsync(request);
            return await responseMsg.Content.ReadAsStringAsync();
        }
    }
}
