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
    class Result
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
=======
﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace BitcasaSDK.Dao
{
    public class Result
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        public List<Item> Items { get; set; } 
>>>>>>> folderlisting
    }
}
