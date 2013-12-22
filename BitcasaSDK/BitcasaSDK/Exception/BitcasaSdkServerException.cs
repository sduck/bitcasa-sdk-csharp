using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcasaSdk.Dao;
using BitcasaSdk.Exception;

namespace BitcasaSDK.Exception
{
    public class BitcasaSdkServerException : BitcasaSdkException
    {
        public BitcasaSdkServerException(Error error) : base(error)
        {
        }
    }
}
