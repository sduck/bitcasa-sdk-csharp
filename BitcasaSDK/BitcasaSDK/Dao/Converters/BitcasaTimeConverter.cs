using System;
using Newtonsoft.Json;

namespace BitcasaSDK.Dao.Converters
{
    class BitcasaTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer && reader.TokenType != JsonToken.Float)
            {
                throw new JsonSerializationException("Invalid BitcasaTime value");

            }

            var timestamp = Convert.ToInt64(reader.Value);
            var time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            time = time.AddMilliseconds(timestamp).ToLocalTime();

            return time;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (DateTime);
        }
    }
}
