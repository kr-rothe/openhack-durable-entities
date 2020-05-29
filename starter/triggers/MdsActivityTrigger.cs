/*
[FunctionName("SayHello")]
public static string SayHello([ActivityTrigger] IDurableActivityContext helloContext)
{
    string name = helloContext.GetInput<string>();
    return $"Hello {name}!";
}
*/
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace InventoryManagement
{
    public class MdsActivityTrigger
    {
        [FunctionName("WriteToMds")]
        public static async Task<bool> WriteToMds([ActivityTrigger] IDurableActivityContext context)
        {
            Mds mds = context.GetInput<Mds>();
            return await MdsWriter.performWrite(mds);            
        }
    }

}