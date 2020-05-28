using Newtonsoft.Json;

namespace InventoryManagement
{
    public class Item
    {
        [JsonProperty("inventoryCount")]
        public int InventoryCount { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("upc")]
        public int Upc { get; set; }

        [JsonProperty("productName")]
        public int ProductName { get; set; }

        [JsonProperty("description")]
        public int Description { get; set; }

        [JsonProperty("lastShipmentTimestamp")]
        public int LastShipmentTimestamp { get; set; }

        [JsonProperty("lastUpdateTimestamp")]
        public int LastUpdateTimestamp { get; set; }

    }
}