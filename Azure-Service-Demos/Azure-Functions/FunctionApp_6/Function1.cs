using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp_6
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([QueueTrigger("outqueue", Connection = "conn1")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
