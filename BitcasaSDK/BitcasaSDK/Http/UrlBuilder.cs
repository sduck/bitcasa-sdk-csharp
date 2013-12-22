using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitcasaSdk.Http
{
    class UrlBuilder
    {
        private readonly IDictionary<string, string> _params;

        public string BaseUrl { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        
        public UrlBuilder() : this(null, null, null) { }

        public UrlBuilder(string url, string method) : this(url, method, null) { }

        public UrlBuilder(string baseUrl, string method, string path)
        {
            _params = new Dictionary<string, string>();

            BaseUrl = baseUrl;
            Method = method;
            Path = path;
        }

        public void AddParameter(string name, string value)
        {
            _params.Add(name, value);
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(BaseUrl) || String.IsNullOrEmpty(Method))
            {
                throw new InvalidOperationException("BaseUrl and Method must be provided");
            }

            var urlBuilder = new StringBuilder();

            urlBuilder.Append(BaseUrl).Append(Method);

            if (null != Path)
            {
                urlBuilder.Append(Path);
            }

            var parameters = (from entry in _params
                            select String.Format("{0}={1}", entry.Key, entry.Value)).ToArray();

            if (parameters.Length > 0)
            {
                urlBuilder.Append("?").Append(String.Join("&", parameters));
            }

            return urlBuilder.ToString();

        }
    }
}
