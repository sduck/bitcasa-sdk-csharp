using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BitcasaSDK.Tests.Stubs
{
    class HttpRequestorStub
    {
        public string Response { get; set; }

        public Task<Stream> GetStream(HttpMethod method, string url)
        {
            throw new NotImplementedException();
        }
    }
}
