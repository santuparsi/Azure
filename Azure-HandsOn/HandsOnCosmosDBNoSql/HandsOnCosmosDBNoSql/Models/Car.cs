using Newtonsoft.Json;
namespace HandsOnCosmosDBNoSql.Models
{
    public class Car
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("make")]
        public string Make { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }
    }
}
