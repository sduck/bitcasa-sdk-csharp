using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using BitcasaSDK.Dao;
using BitcasaSDK.Dao.Converters;
using BitcasaSDK.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BitcasaSDK
{
    public class BitcasaClient
    {
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

        public BitcasaClient(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _httpRequestor = new HttpRequestor();
        }

        public string GetAuthenticateUrl()
        {
            var urlBuilder = new UrlBuilder(Constants.ApiUrl, Constants.Methods.Oauth);
            urlBuilder.AddParameter(Constants.Parameters.ClientId, _clientId);
            urlBuilder.AddParameter(Constants.Parameters.ResponseType, "code");

            return urlBuilder.ToString();
        }

        public async Task RequestAccessToken(string authorizationCode)
        {
            var urlBuilder = new UrlBuilder(Constants.ApiUrl, Constants.Methods.AccessToken);
            urlBuilder.AddParameter(Constants.Parameters.Secret, _clientSecret);
            urlBuilder.AddParameter(Constants.Parameters.Code, authorizationCode);

            var result = await _httpRequestor.GetString(HttpMethod.Get, urlBuilder.ToString());

            var response = JsonConvert.DeserializeObject<Response>(result);

            if (null == response)
            {
                throw new InvalidOperationException("Failed parsing the response from Bitcasa");
            }

            if (response.HasError())
            {
                throw new AuthenticationException(String.Format("Failed to get token: {0}", response.Error));
            }

            _accessToken = response.Result.AccessToken;
        }

        public async Task<List<Item>> GetFoldersList(string path)
        {
            path = path ?? "";

            var urlBuilder = new UrlBuilder(Constants.ApiUrl, Constants.Methods.Folders);
            urlBuilder.AddParameter(Constants.Parameters.AccessToken, _accessToken);

            var result = await _httpRequestor.GetString(HttpMethod.Get, urlBuilder.ToString());

            var response = JsonConvert.DeserializeObject<Response>(result, new ItemConverter(), new BitcasaTimeConverter());

            return response.Result.Items;
        }
    }
}
