using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Table;
using NateFunctionApp.Models;

namespace NateFunctionApp
{
    public static class CreateAzureTable
    {
        [FunctionName("CreateAzureTable")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string tableName = "natemaintable";

            CloudTable table = await CreateTableAsync(tableName);
            await InsertCustomer(table);

            return new OkObjectResult("Created db and ran queries.");
        }

        private static async Task<CloudTable> CreateTableAsync(string tableName)
        {
            string storageConnectionString = System.Environment.GetEnvironmentVariable("TableStorageConnectionString");

            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccount(storageConnectionString);

            // Create a table
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            // Get the table reference
            CloudTable table = tableClient.GetTableReference(tableName);
            if (await table.CreateIfNotExistsAsync())
            {
                Console.WriteLine("Created Table named: {0}", tableName);
            }
            else
            {
                Console.WriteLine("Table {0} already exists", tableName);
            }

            Console.WriteLine();
            return table;
        }

        /**
         * Creates a cloud storage account
         */
        private static CloudStorageAccount CreateStorageAccount(string connectionString)
        {
            try
            {
                return CloudStorageAccount.Parse(connectionString);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid storage account information provided.");
                throw;
            }
        }

        /**
         * Inserts a user entity
         */
        private static async Task InsertCustomer(CloudTable table)
        {
            CustomerEntity customer = new CustomerEntity("Harp", "Walter")
            {
                Email = "Walter@somedomain.com",
                PhoneNumber = "425-555-0101"
            };

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(customer);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);

                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }
    }
}
