using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Queues; // Namespace for Queue storage types
using Azure.Storage.Queues.Models; // Namespace for PeekedMessage
namespace HandsOnAPIUsingQueue_Demo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public QueueController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Create a message queue
        [HttpPost,Route("CreateQueue/{QueueName}")]
        public IActionResult CreateQueue(string QueueName)
        {
            // Get the connection string from appsettings
            string connectionString =_configuration.GetValue<string>("StorageConnectionString");
            // Instantiate a QueueClient which will be used to create and manipulate the queue
            QueueClient queueClient = new QueueClient(connectionString, QueueName);
            // Create the queue
            queueClient.CreateIfNotExists();
            if(queueClient.Exists())
            {
                return StatusCode(200, ($"Queue created: '{queueClient.Name}'"));
            }
            else
            {
                return StatusCode(404, ($"Error Occcured while Creating the Queue '{queueClient.Name}'"));
            }

        }
        // Insert a message into a queue
        [HttpPost, Route("InsertMessage/{queueName}/{message}")]
        public IActionResult InsertMessage(string queueName, string message)
        {
            // Get the connection string from appsettings
            string connectionString = _configuration.GetValue<string>("StorageConnectionString");

            // Instantiate a QueueClient which will be used to create and manipulate the queue
            QueueClient queueClient = new QueueClient(connectionString, queueName);

            // Create the queue if it doesn't already exist
            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                // Send a message to the queue
                queueClient.SendMessage(message);
            }

            return StatusCode(200,($"Inserted: {message}"));
        }
        //Peek at a message in the queue
        [HttpGet, Route("PeekMessage/{queueName}")]
        public IActionResult PeekMessage(string queueName)
        {
            // Get the connection string from appsettings
            string connectionString = _configuration.GetValue<string>("StorageConnectionString");

            // Instantiate a QueueClient which will be used to create and manipulate the queue
            QueueClient queueClient = new QueueClient(connectionString, queueName);

            // Create the queue if it doesn't already exist
            queueClient.CreateIfNotExists();
            string message = "";

            if (queueClient.Exists())
            {
                // Peek at the next message
                PeekedMessage[] peekedMessage = queueClient.PeekMessages();
                message = $"Peeked message: '{peekedMessage[0].Body}'";
            }

            return StatusCode(200, message);
        }
        // Update an existing message in the queue
        [HttpPut, Route("UpdateMessage/{queueName}")]
        public IActionResult UpdateMessage(string queueName)
        {
            // Get the connection string from appsettings
            string connectionString = _configuration.GetValue<string>("StorageConnectionString");

            // Instantiate a QueueClient which will be used to manipulate the queue
            QueueClient queueClient = new QueueClient(connectionString, queueName);

            if (queueClient.Exists())
            {
                // Get the message from the queue
                QueueMessage[] message = queueClient.ReceiveMessages();

                // Update the message contents
                queueClient.UpdateMessage(message[0].MessageId,
                        message[0].PopReceipt,
                        "Hello Sachin",TimeSpan.Zero
                    );
            }
            return StatusCode(200, "Message Updated");
        }
        //Process and remove a message from the queue
        [HttpDelete, Route("UpdateMessage/{queueName}")]
        public IActionResult DequeueMessage(string queueName)
        {
            // Get the connection string from appsettings
            string connectionString = _configuration.GetValue<string>("StorageConnectionString");

            // Instantiate a QueueClient which will be used to manipulate the queue
            QueueClient queueClient = new QueueClient(connectionString, queueName);
            string message = "";
            if (queueClient.Exists())
            {
                // Get the next message
                QueueMessage[] retrievedMessage = queueClient.ReceiveMessages();
                message= $"Dequeued message: '{retrievedMessage[0].Body}'";
                // Delete the message
                queueClient.DeleteMessage(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);
            }
            return StatusCode(200, message);
        }

    }



}
