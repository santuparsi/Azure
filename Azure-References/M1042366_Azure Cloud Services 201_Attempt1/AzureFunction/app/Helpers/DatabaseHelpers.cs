using Microsoft.Azure.Cosmos;
using NateFunctionApp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NateFunctionApp.Helpers
{
    class DatabaseHelpers
    {
        /**
         * Initializes the database - creates the database and container and runs a test query
         */
        public static async Task InitDatabase(CosmosClient cosmosClient) {

            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(System.Environment.GetEnvironmentVariable("DatabaseId"));

            // Create a new container
            Container container = await database.CreateContainerIfNotExistsAsync(new ContainerProperties(System.Environment.GetEnvironmentVariable("ContainerId"), "/LastName"));

            // Seed the database
            await SeedDatabaseAsync(cosmosClient);

            await RunTestQuery(container);
        }

        /**
         * Runs a test query
         */
        public static async Task RunTestQuery(Container container)
        {
            // Run a test query for the Andersen family
            var sqlQueryText = "SELECT * FROM c WHERE c.LastName = 'Andersen'";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Family> queryResultSetIterator = container.GetItemQueryIterator<Family>(queryDefinition);

            List<Family> families = new List<Family>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Family> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Family family in currentResultSet)
                {
                    families.Add(family);
                }
            }
        }

        /**
         * Seed the database
         */
        public static async Task SeedDatabaseAsync(CosmosClient cosmosClient)
        {
            Container container = cosmosClient.GetContainer(System.Environment.GetEnvironmentVariable("DatabaseId"), System.Environment.GetEnvironmentVariable("ContainerId"));

            // Create a family object for the Andersen family
            Family andersenFamily = new Family
            {
                Id = "Andersen.1",
                LastName = "Andersen",
                Parents = new Parent[]
                {
                   new Parent { FirstName = "Thomas" },
                   new Parent { FirstName = "Mary Kay" }
                },
                Children = new Child[]
                {
               new Child
                {
                    FirstName = "Henriette Thaulow",
                    Gender = "female",
                    Grade = 5,
                    Pets = new Pet[]
                    {
                        new Pet { GivenName = "Fluffy" }
                    }
                }
                },
                Address = new Address { State = "WA", County = "King", City = "Seattle" },
                IsRegistered = false
            };

            try
            {
                // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen".
                ItemResponse<Family> andersenFamilyResponse = await container.CreateItemAsync<Family>(andersenFamily, new PartitionKey(andersenFamily.LastName));
                // Note that after creating the item, we can access the body of the item with the Resource property of the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", andersenFamilyResponse.Resource.Id, andersenFamilyResponse.RequestCharge);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                Console.WriteLine("Item in database with id: {0} already exists\n", andersenFamily.Id);
            }
        }
    }
}
