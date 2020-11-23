using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AlarmSystem.Functions.Subscription.DeleteAlarmSubscription
{
    public class DeleteAlarmSubscription
    {
        [FunctionName("SubscribeToAlarm")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "alarmsubs")] HttpRequest req,
            ILogger log) 
            {
                return new NoContentResult();
            }
    }
}