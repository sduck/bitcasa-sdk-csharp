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

            new Auth().Run(args[0], args[1]).Wait();

            Console.ReadLine();
        }

        private async Task Run(string clientId, string clientSecret)
        {
            var client = new BitcasaClient(clientId, clientSecret);

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
