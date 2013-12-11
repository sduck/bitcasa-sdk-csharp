using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
using System.Threading.Tasks;
=======
>>>>>>> folderlisting

namespace BitcasaSDK.Http
{
    class UrlBuilder
    {
<<<<<<< HEAD
        private IDictionary<string, string> _params;
=======
        private readonly IDictionary<string, string> _params;
>>>>>>> folderlisting

        public string BaseUrl { get; set; }
        public string Method { get; set; }
        
        public UrlBuilder()
        {
            _params = new Dictionary<string, string>();
        }

        public UrlBuilder(string url, string method)
            : this()
        {
            BaseUrl = url;
            Method = method;
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

            var parameters = (from entry in _params
                            select String.Format("{0}={1}", entry.Key, entry.Value)).ToArray();

<<<<<<< HEAD
            if (parameters.Count() > 0)
=======
            if (parameters.Length > 0)
>>>>>>> folderlisting
            {
                urlBuilder.Append("?").Append(String.Join("&", parameters));
            }

            return urlBuilder.ToString();

        }
    }
}
