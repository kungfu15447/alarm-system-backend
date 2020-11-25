using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AlarmSystem.Functions.Subscription
{
    public class GetAlarmSubscriptions
    {
        private IWatchService _watchService;
        public GetAlarmSubscriptions(IWatchService watchService)
        {
            _watchService = watchService;
        }

        [FunctionName("GetAlarmSubscriptions")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "alarmsubs/{watchId}")] HttpRequest req,
            ILogger log, string watchId)
        {
            try 
            {
                List<AlarmWatch> subscriptions = _watchService.GetAlarmSubscriptionsFromWatch(watchId);

                if (subscriptions.Count == 0) 
                {
                    return new NoContentResult();
                }
                return new OkObjectResult(subscriptions);
            }catch(InvalidDataException e) 
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}