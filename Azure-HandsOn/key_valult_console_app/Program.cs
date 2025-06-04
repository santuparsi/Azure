using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Net.Sockets;

namespace key_valult_console_app
{
    internal class Program
    {
        public static async Task GetSecret()
        {
            const string secretName = "secret1";
           
            var kvUri = $"https://keyvault51920231.vault.azure.net/";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            var secret = await client.GetSecretAsync(secretName);
            Console.WriteLine($"Your secret is '{secret.Value.Value}'.");
        }
        static async Task Main(string[] args)
        {
            const string secretName = "mySecret";
            var keyVaultName = "keyvault51920231";
            var kvUri = $"https://keyvault51920231.vault.azure.net/";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            Console.Write("Input the value of your secret > ");
            var secretValue = Console.ReadLine();
            Console.Write($"Creating a secret in {keyVaultName} called '{secretName}' with the value '{secretValue}' ...");
            //Save a secret
            await client.SetSecretAsync(secretName, secretValue);
            Console.WriteLine(" done.");
            Console.WriteLine("Forgetting your secret.");
            secretValue = string.Empty;
            Console.WriteLine($"Your secret is '{secretValue}'.");
            Console.WriteLine($"Retrieving your secret from {keyVaultName}.");
            // Retrieve a secret
            var secret = await client.GetSecretAsync(secretName);
            Console.WriteLine($"Your secret is '{secret.Value.Value}'.");

            //Delete a secret

            //Console.Write($"Deleting your secret from {keyVaultName} ...");
            //DeleteSecretOperation operation = await client.StartDeleteSecretAsync(secretName);
            //// You only need to wait for completion if you want to purge or recover the secret.
            //await operation.WaitForCompletionAsync();
            //Console.WriteLine(" done.");

            //Console.Write($"Purging your secret from {keyVaultName} ...");
            //await client.PurgeDeletedSecretAsync(secretName);
            //Console.WriteLine(" done.");
            //await GetSecret();
        }
    }
}