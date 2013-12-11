using System.IO;

namespace BitcasaSDK.Tests.Helpers
{
    class StreamHelper
    {
        public static Stream GetFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
