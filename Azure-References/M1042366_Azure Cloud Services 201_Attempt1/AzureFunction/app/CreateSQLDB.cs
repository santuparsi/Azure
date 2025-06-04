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
using NateFunctionApp.Helpers;
using NateFunctionApp.Models;
using System.Collections.Generic;

namespace NateFunctionApp
{
    public static class CreateSQLDB
    {
        [FunctionName("CreateSQLDB")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            CosmosClient cosmosClient = new CosmosClient(System.Environment.GetEnvironmentVariable("SQLEndpointUrl"), System.Environment.GetEnvironmentVariable("SQLPrimaryKey"));

            await DatabaseHelpers.InitDatabase(cosmosClient);

            return new OkObjectResult("Created database and items.");
        }


    }
}
