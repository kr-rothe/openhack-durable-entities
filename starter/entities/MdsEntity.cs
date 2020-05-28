using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Collections.Generic;

namespace InventoryManagement
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MdsEntity : IMdsEntity
    {
        [JsonProperty("storeId")]
        public int StoreId { get; set; }

        [JsonProperty("divisionId")]
        public int DivisionId { get; set; }

        [JsonProperty("items")]
        public Dictionary<string,Item> Items { get; set; }

        [FunctionName(nameof(MdsEntity))]
        public static Task mdsentity([EntityTrigger] IDurableEntityContext ctx)
            => ctx.DispatchAsync<MdsEntity>();

        public void updateInventoryCount(int amount)
        {
            this.InventoryCount += amount;
        }

    }
}