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
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Cassandra;
using Cassandra.Mapping;
using NateFunctionApp.Models;

namespace NateFunctionApp
{
    public static class CreateCassandraDB
    {
        [FunctionName("CreateCassandraDB")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var options = new Cassandra.SSLOptions(SslProtocols.Tls12, true, ValidateServerCertificate);
            var cassandraContactPoint = System.Environment.GetEnvironmentVariable("CassandraContactPoint");
            var username = System.Environment.GetEnvironmentVariable("CassandraUsername");
            var password = System.Environment.GetEnvironmentVariable("CassandraPassword");
            int port = 10350;

            // Connect to Cassandra DB
            options.SetHostNameResolver((ipAddress) => cassandraContactPoint);
            Cluster cluster = Cluster.Builder().WithCredentials(username, password).WithPort(port).AddContactPoint(cassandraContactPoint).WithSSL(options).Build();
            Cassandra.ISession session = cluster.Connect();

            // Create a keyspace
            session.Execute("CREATE KEYSPACE uprofile WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', 'datacenter1' : 1 };");
            session.Execute("CREATE TABLE IF NOT EXISTS uprofile.user (user_id int PRIMARY KEY, user_name text, user_bcity text)");

            session = cluster.Connect("uprofile");
            IMapper mapper = new Mapper(session);

            // Insert Data into user table
            mapper.Insert<Models.User>(new Models.User(1, "LyubovK", "Dubai"));
            mapper.Insert<Models.User>(new Models.User(2, "JiriK", "Toronto"));
            Console.WriteLine("Inserted data into user table");

            // Query for data
            foreach (Models.User user in mapper.Fetch<Models.User>("Select * from user"))
            {
                Console.WriteLine(user);
            }

            return new OkObjectResult("Created database and items.");
        }

        public static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
    }
}
