using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace InventoryManagement
{
    public static class OnHandDbTrigger_HttpStart
    {

        [FunctionName("OnHandDbTrigger_HttpStart")]
        public static void Run(
            [CosmosDBTrigger(
                    databaseName: "inventory",
                    collectionName: "onHand",
                    ConnectionStringSetting = "CosmosDBConnection",
                    LeaseCollectionName="onHand_lease",
                    CreateLeaseCollectionIfNotExists = true)]
                    IReadOnlyList<Document> input,
                    ILogger logger)
        {

            logger.LogInformation("Number of documents changed in onHand container : {}", input.Count);

            foreach (Document doc in input)
            {
                logger.LogInformation("Document Id {}", doc.Id);
            }
        }
    }
}