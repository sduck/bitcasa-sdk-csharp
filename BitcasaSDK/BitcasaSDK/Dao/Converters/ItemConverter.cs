using System;
using Newtonsoft.Json.Linq;

namespace BitcasaSdk.Dao.Converters
{
    class ItemConverter : JsonItemConverter<Item>
    {
        protected override Item Create(Type objectType, JObject jObject)
        {
            {
                var type = (string)jObject.Property("category");

                switch (type)
                {
                    case "folders":
                        return new Folder();

                    default:
                        return new Item();
                }
            }
        }
    }
}
