using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Collections.Generic;

namespace InventoryManagement
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StoreEntity : IStoreEntity
    {
        [JsonProperty("storeId")]
        public int StoreId { get; set; }

        [JsonProperty("divisionId")]
        public int DivisionId { get; set; }

        [JsonProperty("items")]
        public Dictionary<string, Item> Items = new Dictionary<string, Item>();

        [FunctionName(nameof(StoreEntity))]
        public static Task mdsentity([EntityTrigger] IDurableEntityContext ctx)
            => ctx.DispatchAsync<StoreEntity>();
        
        public async Task<Item> getStoreEntityItem(string id)
        {
            if (Items.ContainsKey(id)) 
            {
                return Items[id];
            }
            else 
            {
                return null;
            }

        }
        public void updateInventoryCount(EventSchema schema)
        {
            if (Items.ContainsKey(schema.id))
            {

                var item = Items[schema.id];
                item.InventoryCount = schema.inventoryCount;
                item.LastUpdateTimestamp = schema.lastUpdateTimestamp;
                item.Description = schema.description;
                item.ProductName = schema.productName;
                item.Upc = schema.upc;

                if (schema.type.Equals("shipment"))
                {
                    item.InventoryCount += schema.inventoryCount;
                    item.LastShipmentTimestamp = schema.lastShipmentTimestamp;
                }

            }
            else
            {
                var item = new Item();
                item.Id = schema.id;
                item.InventoryCount = schema.inventoryCount;
                item.LastUpdateTimestamp = schema.lastUpdateTimestamp;
                item.Description = schema.description;
                item.ProductName = schema.productName;
                item.Upc = schema.upc;
                
                if (schema.type.Equals("shipment"))
                {
                    item.LastShipmentTimestamp = schema.lastShipmentTimestamp;
                }

                Items.Add(schema.id, item);
            }
        }

    }
}