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
            string data = context.GetInput<string>();

            var StoreId = new EntityId("Counter", "myCounter");
            var proxy = context.CreateEntityProxy<ICounter>(StoreId);

            // One-way signal to the entity - does not await a response
            proxy.Add(1);

            // Two-way call to the entity which returns a value - awaits the response
            int currentValue = await proxy.Get().ConfigureAwait(false);

            return "Task";
        }
    }
}