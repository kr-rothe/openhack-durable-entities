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

            // Two-way call to the entity which returns a value - awaits the response
            //int currentValue = await storeProxy.Get().ConfigureAwait(false);

            return "Task";
        }
    }
}