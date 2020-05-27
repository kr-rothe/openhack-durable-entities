using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace InventoryManagement
{
    public static class ShippingDbTrigger_HttpStart
    {

        [FunctionName("ShippingDbTrigger_HttpStart")]
        public static void Run(
            [CosmosDBTrigger(
                    databaseName: "inventory",
                    collectionName: "shipments",
                    ConnectionStringSetting = "CosmosDBConnection",
                    LeaseCollectionName="shipping_lease",
                    CreateLeaseCollectionIfNotExists = true)]
                    IReadOnlyList<Document> input,
                    ILogger logger)
        {

            logger.LogInformation("Number of documents changed in Shipments container: {0}", input.Count);

            foreach (Document doc in input)
            {
                logger.LogInformation("Document Id {0}", doc.Id);
            }
        }
    }
}