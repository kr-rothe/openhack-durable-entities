using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace InventoryManagement
{
    public static class OnHandChangeTrigger
    {
        [FunctionName("OnHandChangeTrigger")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: Constants.databaseName,
            collectionName: Constants.collectionOnHand,
            ConnectionStringSetting = "CosmosDBConnection",
            LeaseCollectionName = "onhand_leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> documents,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            if (documents != null && documents.Count > 0)
            {
                log.LogInformation("Documents modified: {count}", documents.Count);
                foreach (var document in documents)
                {
                    log.LogInformation("First document Id: {id}", document.Id);
                    OnHandDocument onHandDocument = JsonConvert.DeserializeObject<OnHandDocument>(document.ToString());
                    string instanceId = await starter.StartNewAsync<EventSchema>("MdsOrchestration", null, getEventSchema(onHandDocument)).ConfigureAwait(false);
                }

            }
        }

        private static EventSchema getEventSchema(OnHandDocument onHandDocument)
        {

            EventSchema schema = new EventSchema();
            schema.storeId = onHandDocument.StoreId;
            schema.id = onHandDocument.Id;
            schema.upc = onHandDocument.Upc;
            schema.inventoryCount = onHandDocument.InventoryCount;
            schema.type = "onHand";
            schema.productName = onHandDocument.ProductName;
            schema.description = onHandDocument.ProductName;
            schema.lastUpdateTimestamp = onHandDocument.LastUpdateTimestamp;

            return schema;
        }

    }
}