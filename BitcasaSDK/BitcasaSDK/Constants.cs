
namespace BitcasaSdk
{
    public class Constants
    {
        public const string ApiUrl = "https://developer.api.bitcasa.com/v1";

        public class Methods
        {
            public const string Oauth = "/oauth2/authenticate";
            public const string AccessToken = "/oauth2/access_token";
            public const string Folders = "/folders";
        }

        public class Parameters
        {
            public const string ClientId = "client_id";
            public const string ResponseType = "response_type";
            public const string Secret = "secret";
            public const string Code = "code";
            public const string AccessToken = "access_token";
        }
    }
}
