using Microsoft.Azure.WebJobs;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace InventoryManagement
{
    public static class ShipmentChangeTrigger
    {
        [FunctionName("ShipmentChangeTrigger")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: Constants.databaseName,
            collectionName: Constants.collectionShipments,
            ConnectionStringSetting = "CosmosDBConnection",
            LeaseCollectionName = "shipments_leases",
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
                    ShipmentDocument shipmentDocument = JsonConvert.DeserializeObject<ShipmentDocument>(document.ToString());
                    foreach(var schema in getEventSchema(shipmentDocument)) {
                        string instanceId = await starter.StartNewAsync<EventSchema>("MdsOrchestration", null, schema).ConfigureAwait(false);
                    }        
                }
            }
        }

        private static List<EventSchema> getEventSchema(ShipmentDocument shipmentDocument)
        {

            List<EventSchema> schemas = new List<EventSchema>();

            foreach (var item in shipmentDocument.Items)
            {

                EventSchema schema = new EventSchema();
                schema.storeId = shipmentDocument.StoreId;
                schema.id = $"{shipmentDocument.DivisionId}:{shipmentDocument.StoreId}:{item.upc}"; 
                schema.inventoryCount = item.shipmentAmount;
                schema.type = "shipment";
                schema.upc = item.upc;
                schema.lastUpdateTimestamp = shipmentDocument.ArrivalTimestamp;
                schema.lastShipmentTimestamp = shipmentDocument.ArrivalTimestamp;
                schemas.Add(schema);
            }


            return schemas;
        }

    }
}