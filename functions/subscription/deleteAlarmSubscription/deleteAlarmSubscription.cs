using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlarmSystem.Functions.Subscription.DeleteAlarmSubscription
{
    public class DeleteAlarmSubscription
    {
        private IWatchService _watchService;

        public DeleteAlarmSubscription(IWatchService watchService)
        {
            _watchService = watchService;
        }

        [FunctionName("SubscribeToAlarm")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "alarmsubs")] HttpRequest req,
            ILogger log) 
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                DeleteAlarmSubscriptionModel dasm = JsonConvert.DeserializeObject<DeleteAlarmSubscriptionModel>(requestBody);


                return new NoContentResult();
            }

            private AlarmWatch ParseFunctionModelToDeleteModel(DeleteAlarmSubscriptionModel dasm)
            {
                return new AlarmWatch();
            }
    }
}