using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace InventoryManagement
{
    public static class OnHandChangeTrigger 
    {
        [FunctionName("OnHandChangeTrigger")]
        public static void Run([CosmosDBTrigger(
            databaseName: Constants.databaseName,
            collectionName: Constants.collectionOnHand,
            ConnectionStringSetting = "CosmosConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> documents,
            ILogger log)
        {
            if (documents != null && documents.Count > 0)
            {
                log.LogInformation("Documents modified: {count}", documents.Count);
                log.LogInformation("First document Id: {id}", documents[0].Id);
            }
        }
    }

    public static class ShipmentChangeTrigger
    {
        [FunctionName("ShipmentChangeTrigger")]
        public static void Run([CosmosDBTrigger(
            databaseName: Constants.databaseName,
            collectionName: Constants.collectionShipments,
            ConnectionStringSetting = "CosmosConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> documents,
            ILogger log)
        {
            if (documents != null && documents.Count > 0)
            {
                log.LogInformation("Documents modified: {count}", documents.Count);
                log.LogInformation("First document Id: {id}", documents[0].Id);
            }
        }
    }
}