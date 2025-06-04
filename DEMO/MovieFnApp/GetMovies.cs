using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MovieFnApp.Entities;
using System.Linq;

namespace MovieFnApp
{
    public static class GetMovies
    {
        [FunctionName("GetMovies")]
        public static async Task<IActionResult> GetAllMovies(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            await using (MovieDbContext context = new MovieDbContext())
            {
                var movies = context.Movies.ToList();
                return new OkObjectResult(movies);
            }

            
        }
    }
}
