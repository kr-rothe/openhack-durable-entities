using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Collections.Generic;
using System;

namespace InventoryManagement
{
    public class OnHandDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("divisionId")]
        public string DivisionId { get; set; }

        [JsonProperty("storeId")]
        public string StoreId { get; set; }

        [JsonProperty("upc")]
        public string Upc { get; set; }

        [JsonProperty("inventoryCount")]
        public int InventoryCount { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("lastUpdateTimestamp")]
        public DateTime LastUpdateTimestamp { get; set; }
    }
}