using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AlarmSystem.Functions.Subscriptions
{
    public class GetMachineSubscriptions
    {
        public GetMachineSubscriptions()
        {

        }

        [FunctionName("GetMachineSubscriptions")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "machinesubs/{watchId}")] HttpRequest req,
            ILogger log, string watchId)
        {
            return new OkResult();
        }
    }
}