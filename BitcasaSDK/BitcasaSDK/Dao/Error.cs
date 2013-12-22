using System;

namespace BitcasaSdk.Dao
{
    public class Error
    {
        public string Message { get; set; }

        public Int32 Code { get; set; }

        public override string ToString()
        {
            return String.Format("Bitcasa Error {0}: {1}", Code, Message);
        }
    }
}
