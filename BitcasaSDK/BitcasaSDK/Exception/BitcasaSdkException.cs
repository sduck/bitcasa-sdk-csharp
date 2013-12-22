using BitcasaSdk.Dao;

namespace BitcasaSdk.Exception
{
    public class BitcasaSdkException : System.Exception
    {
        public Error Error
        {
            get; private set;
        }

        public BitcasaSdkException(string message) : base(message) { }

        public BitcasaSdkException(Error error) : base(error.Message)
        {
            Error = error;
        }
    }
}
