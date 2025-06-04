using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using NateFunctionApp.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using NateFunctionApp.Helpers;
using System.Security.Authentication;

namespace NateFunctionApp
{
    public static class CreateMongoDB
    {
        [FunctionName("CreateMongoDB")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string connectionString = @Environment.GetEnvironmentVariable("MongoDBConnectionString");
            MongoClientSettings settings = MongoClientSettings.FromUrl( new MongoUrl(connectionString) );

            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);

            // Get the database, creating it if it doesn't exist yet
            var database = mongoClient.GetDatabase("TestDatabase");

            // Get the collection, or create it if it doesn't exist yet
            var collection = database.GetCollection<BsonDocument>("TestCollection");

            // Create a document and insert into the database
            var user = new BsonDocument
            {
                { "user_id", "1" },
                { "username", "nlabrake" },
                { "city", "Stanwood" },
                { "state", "WA" }
            };
            await collection.InsertOneAsync(user);

            // Query for the document
            var document = await collection.Find(new BsonDocument()).FirstOrDefaultAsync();
            Console.WriteLine(document.ToString());

            return new OkObjectResult("Created database and items.");
        }
    }
}
