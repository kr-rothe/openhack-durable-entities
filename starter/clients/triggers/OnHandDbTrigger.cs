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
        public static async Task Run(
            [CosmosDBTrigger(
                    databaseName: "inventory",
                    collectionName: "onHand",
                    ConnectionStringSetting = "CosmosDBConnection",
                    LeaseCollectionName="leases",
                    CreateLeaseCollectionIfNotExists = true)]
                    IReadOnlyList<Document> input,
                    ILogger logger)       
        {

            logger.LogInformation( "Count : " + input.Count);

            foreach (Document doc in input)
            {
                logger.LogInformation(doc.Id);
            }
        }
    }
}