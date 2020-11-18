using System.Threading.Tasks;
using AlarmSystem.Core.Application;
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
            return new OkResult();
        }
    }
}