using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp3
{
    public static class Function1
    {
        [FunctionName("GetName")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];



            string responseMessage = "Hello " + name;

            return new OkObjectResult(responseMessage);
        }
        [FunctionName("Sum")]
        public static async Task<IActionResult> Sum(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            int n1 = int.Parse(req.Query["n1"]);
            int n2 = int.Parse(req.Query["n2"]);
            int result = n1 + n2;


            string responseMessage = "addition " + result;

            return new OkObjectResult(responseMessage);
        }
        [FunctionName("GetFlowers")]
        public static async Task<IActionResult> GetFlowers(
          [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
          ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");




            var responseMessage = new string[] { "Rose", "Lilly", "Jasmine", "Tulips" };

            return new OkObjectResult(responseMessage);
        }
    }
}
