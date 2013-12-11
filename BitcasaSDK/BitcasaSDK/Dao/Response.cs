<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BitcasaSDK.Dao
{
    [DataContract]
    class Response
    {
        [DataMember(Name = "result")]
        public Result Result { get; set; }

        [DataMember(Name = "error")]
=======
﻿
namespace BitcasaSDK.Dao
{
    public class Response
    {
        public Result Result { get; set; }

>>>>>>> folderlisting
        public Error Error { get; set; }

        public bool HasError()
        {
            return (null != Error);
        }
    }
}
