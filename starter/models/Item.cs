using System;
using Newtonsoft.Json;

namespace InventoryManagement
{
    public class Item
    {
        [JsonProperty("inventoryCount")]
        public int InventoryCount { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("upc")]
        public string Upc { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("lastShipmentTimestamp")]
        public DateTime LastShipmentTimestamp { get; set; }

        [JsonProperty("lastUpdateTimestamp")]
        public DateTime LastUpdateTimestamp { get; set; }

    }
}