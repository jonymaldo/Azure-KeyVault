using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static object sec;


        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {

            var storageKey = await GetKey();
            Console.Write(storageKey);

        }

        private static async Task<string> GetKey()
        {

            var client = new KeyVaultClient(
            new KeyVaultClient.AuthenticationCallback(GetAccessTokenAsync),
            new System.Net.Http.HttpClient());

            var vaultUrl = "";
            var secretUrl = "";

            var secret = Task.Run(() => client.GetSecretAsync(vaultUrl, secretUrl)).Result;

            return secret.Value;

        }
        
        private static async Task<string> GetAccessTokenAsync(
       string authority,
       string resource,
       string scope)
        {

            var clientId = "";
            var clientSecret = "";

            var clientCredential = new ClientCredential(
                clientId,
                clientSecret);

            var context = new AuthenticationContext(
                authority,
                TokenCache.DefaultShared);

            var result = await context.AcquireTokenAsync(
                resource,
                clientCredential);

            return result.AccessToken;
        }


    }
}
