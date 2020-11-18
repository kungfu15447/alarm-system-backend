using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AlarmSystem.Functions.Subscription.DeleteMachineSubscription
{
    public class DeleteMachineSubscription
    {
        public DeleteMachineSubscription()
        {

        }

        [FunctionName("DeleteMachineSubscription")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "machinesubs")] HttpRequest req,
            ILogger log)
        {
            return new OkResult();
        }
    }
}