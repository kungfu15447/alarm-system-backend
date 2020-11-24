using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Core.Entity.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AlarmSystem.Functions.Subscription
{
    public class GetMachineSubscriptions
    {
        private IWatchService _watchService;
        public GetMachineSubscriptions(IWatchService watchService)
        {
            _watchService = watchService;
        }

        [FunctionName("GetMachineSubscriptions")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "machinesubs/{watchId}")] HttpRequest req,
            ILogger log, string watchId)
        {
            try
            {
                List<MachineWatch> subscriptions = _watchService.GetMachineSubscriptionsFromWatch(watchId);

                if (subscriptions.Count == 0)
                {
                    return new NoContentResult();
                }
                return new OkObjectResult(subscriptions);
            }catch(InvalidDataException ex) {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}