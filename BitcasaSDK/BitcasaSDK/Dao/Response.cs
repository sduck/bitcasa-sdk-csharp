
namespace BitcasaSDK.Dao
{
    public class Response
    {
        public Result Result { get; set; }

        public Error Error { get; set; }

        public bool HasError()
        {
            return (null != Error);
        }
    }
}
