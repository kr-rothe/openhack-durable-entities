
using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;


namespace InventoryManagement
{
    public static class MdsWriter
    {
        private static Container _container;
        private static readonly string databaseId = "inventory";
        private static readonly string containerId = "mds";
        static MdsWriter() 
        {                           
            var endpoint =  Environment.GetEnvironmentVariable("cosmosEndpoint", EnvironmentVariableTarget.Process);     
            var authKey =  Environment.GetEnvironmentVariable("cosmosAuthKey", EnvironmentVariableTarget.Process);     
            var _client = new CosmosClient(endpoint, authKey);
            Database database =  _client.GetDatabase(databaseId);
            _container = database.GetContainer(containerId);
        }
        public static async Task<bool> performWrite(Mds mdsToWrite)
        {            
            try 
            {
                ItemResponse<Mds> response = await _container.UpsertItemAsync(mdsToWrite, new PartitionKey(mdsToWrite.id));                
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
    }

}