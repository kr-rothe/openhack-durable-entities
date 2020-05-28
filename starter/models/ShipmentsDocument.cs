using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace InventoryManagement
{
    public class ShipmentItem
    {
        public string upc { get; set; }
        public int shipmentAmount { get; set; }

    }

    public class ShipmentDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("divisionId")]
        public string DivisionId { get; set; }

        [JsonProperty("storeId")]
        public string StoreId { get; set; }

        [JsonProperty("distributorId")]
        public string DistributorId { get; set; }

        [JsonProperty("items")]
        public List<ShipmentItem> Items { get; set; }
        
        [JsonProperty("arrivalTimestamp")]
        public DateTime ArrivalTimestamp { get; set; }
    }
}