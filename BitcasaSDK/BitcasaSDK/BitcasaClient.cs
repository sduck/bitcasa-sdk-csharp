using System.Net.Http;
using System.Security.Authentication;
using BitcasaSDK.Dao;
<<<<<<< HEAD
using BitcasaSDK.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
=======
using BitcasaSDK.Dao.Converters;
using BitcasaSDK.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
>>>>>>> folderlisting

namespace BitcasaSDK
{
    public class BitcasaClient
    {
<<<<<<< HEAD
        private string _clientId;
        private string _clientSecret;
        private string _accessToken;
=======
        private readonly string _clientId;
        private readonly string _clientSecret;
        private string _accessToken;
        private IHttpRequestor _httpRequestor;

        public string AccessToken
        {
            get { return _accessToken; }
        }

        public IHttpRequestor HttpRequestor
        {
            get { return _httpRequestor; }
            set { _httpRequestor = value; }
        }
>>>>>>> folderlisting

        public BitcasaClient(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public string GetAuthenticateUrl()
        {
            var urlBuilder = new UrlBuilder(Constants.ApiUrl, Constants.Methods.Oauth);
            urlBuilder.AddParameter(Constants.Parameters.ClientId, _clientId);
            urlBuilder.AddParameter(Constants.Parameters.ResponseType, "code");

            return urlBuilder.ToString();
        }

<<<<<<< HEAD
        public async Task GetAccessToken(string authorizationCode)
=======
        public async Task RequestAccessToken(string authorizationCode)
>>>>>>> folderlisting
        {
            var urlBuilder = new UrlBuilder(Constants.ApiUrl, Constants.Methods.AccessToken);
            urlBuilder.AddParameter(Constants.Parameters.Secret, _clientSecret);
            urlBuilder.AddParameter(Constants.Parameters.Code, authorizationCode);

<<<<<<< HEAD
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, urlBuilder.ToString());
            var responseMsg = await client.SendAsync(request);
            var result = await responseMsg.Content.ReadAsStreamAsync();
            var s = await responseMsg.Content.ReadAsStringAsync();

            var serializer = new DataContractJsonSerializer(typeof(Response));
            object serialized = serializer.ReadObject(result);

            Response response = serialized as Response;

            if (null == response || response.HasError())
            {
                // TODO: Nicely handle response == null
=======
            var result = await _httpRequestor.GetString(HttpMethod.Get, urlBuilder.ToString());

            var response = JsonConvert.DeserializeObject<Response>(result);

            if (null == response)
            {
                throw new InvalidOperationException("Failed parsing the response from Bitcasa");
            }

            if (response.HasError())
            {
>>>>>>> folderlisting
                throw new AuthenticationException(String.Format("Failed to get token: {0}", response.Error));
            }

            _accessToken = response.Result.AccessToken;
        }
<<<<<<< HEAD
=======

        public async Task<Response> GetFoldersList(string path)
        {
            path = path ?? "";

            var urlBuilder = new UrlBuilder(Constants.ApiUrl, Constants.Methods.Folders);
            urlBuilder.AddParameter(Constants.Parameters.AccessToken, _accessToken);

            var result = await _httpRequestor.GetString(HttpMethod.Get, urlBuilder.ToString());

            var response = JsonConvert.DeserializeObject<Response>(result, new ItemConverter(), new BitcasaTimeConverter());

            return response;
        }
>>>>>>> folderlisting
    }
}
