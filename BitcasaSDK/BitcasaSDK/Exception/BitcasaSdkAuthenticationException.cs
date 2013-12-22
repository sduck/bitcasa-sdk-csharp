using BitcasaSdk.Dao;
using BitcasaSdk.Exception;

namespace BitcasaSDK.Exception
{
    public class BitcasaSdkAuthenticationException : BitcasaSdkException
    {
        public BitcasaSdkAuthenticationException(string message) : base(message)
        {
        }

        public BitcasaSdkAuthenticationException(Error error) : base(error)
        {
        }
    }
}
