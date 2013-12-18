using BitcasaSDK;
using System;
using System.Threading.Tasks;

namespace ExampleApp
{
    public class Auth
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Running...");
            Console.WriteLine("----------");

            new Auth().Run().Wait();

            Console.ReadLine();
        }

        private async Task Run()
        {
            var client = new BitcasaClient("eb07ff94", "363b869e03e648f61bcdf1c0ceaeaf35");

            string url = client.GetAuthenticateUrl();
            Console.WriteLine(url);
            Console.WriteLine("Enter auth code");

            string authCode = Console.ReadLine();

            try
            {
                await client.RequestAccessToken(authCode);
                var output = await client.GetFoldersList(null);
                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
