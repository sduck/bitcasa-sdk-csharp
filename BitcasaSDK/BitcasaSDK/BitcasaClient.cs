using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using BitcasaSdk.Dao;
using BitcasaSdk.Dao.Converters;
using System;
using System.Threading.Tasks;
using BitcasaSDK.Exception;
using BitcasaSdk.Exception;
using BitcasaSdk.Http;
using Newtonsoft.Json;

namespace BitcasaSdk
{
    public class BitcasaClient
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private IHttpRequestor _httpRequestor;

        public string AccessToken { get; private set; }

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
                throw new BitcasaSdkAuthenticationException("Failed parsing the response from Bitcasa");
            }

            if (response.HasError())
            {
                throw new BitcasaSdkAuthenticationException(response.Error);
            }

            AccessToken = response.Result.AccessToken;
        }

        public async Task<List<Item>> GetFoldersList(string path)
        {
            path = path ?? "/";

            var urlBuilder = new UrlBuilder(Constants.ApiUrl, Constants.Methods.Folders, path);
            urlBuilder.AddParameter(Constants.Parameters.AccessToken, AccessToken);

            var result = await _httpRequestor.GetString(HttpMethod.Get, urlBuilder.ToString());

            var response = JsonConvert.DeserializeObject<Response>(result, new ItemConverter(), new BitcasaTimeConverter());

            if (response.HasError())
            {
                throw new BitcasaSdkServerException(response.Error);
            }

            return response.Result.Items;
        }
    }
}
