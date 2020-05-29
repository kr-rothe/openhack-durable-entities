using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace InventoryManagement
{
    public static class MdsOrchestration
    {
        [FunctionName("MdsOrchestration")]
        public static async Task<string> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            EventSchema eventSchema = context.GetInput<EventSchema>();

            var StoreId = new EntityId("StoreEntity", eventSchema.storeId);
            var storeProxy = context.CreateEntityProxy<IStoreEntity>(StoreId);

            // One-way signal to the entity - does not await a response
            storeProxy.updateInventoryCount(eventSchema);
            var returnItem = await storeProxy.getStoreEntityItem(eventSchema.id);
            if (returnItem != null)
            {
                Mds mds = new Mds()
                {
                    id = eventSchema.id
                    , storeId = eventSchema.storeId
                    , inventoryCount = returnItem.InventoryCount
                    , productName = returnItem.ProductName
                    , lastShipmentTimestamp = returnItem.LastShipmentTimestamp
                    , lastUpdateTimestamp = returnItem.LastUpdateTimestamp
                    , description = returnItem.Description
                    , upc = returnItem.Upc
                };  

                var returnValue = await context.CallActivityAsync<bool>("WriteToMds", mds);
                //to do handle the returnValue
            }
            

            // Two-way call to the entity which returns a value - awaits the response
            //int currentValue = await storeProxy.Get().ConfigureAwait(false);

            return "Task";
        }
    }
}