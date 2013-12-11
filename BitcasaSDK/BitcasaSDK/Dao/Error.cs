using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BitcasaSDK.Dao
{
    [DataContract]
    class Error
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "code")]
=======

namespace BitcasaSDK.Dao
{
    public class Error
    {
        public string Message { get; set; }

>>>>>>> folderlisting
        public Int32 Code { get; set; }

        public override string ToString()
        {
            return String.Format("Bitcasa Error {0}: {1}", Code, Message);
        }
    }
}
