using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlarmSystem.Functions.Subscription.DeleteAlarmSubscriptionFunction
{
    public class DeleteAlarmSubscription
    {
        private IWatchService _watchService;

        public DeleteAlarmSubscription(IWatchService watchService)
        {
            _watchService = watchService;
        }

        [FunctionName("DeleteAlarmSubscription")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "alarmsubs")] HttpRequest req,
            ILogger log) 
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                DeleteAlarmSubscriptionModel dasm = JsonConvert.DeserializeObject<DeleteAlarmSubscriptionModel>(requestBody);

                AlarmWatch aw;

                try 
                {
                    aw = ParseFunctionModelToDtoModel(dasm);
                } catch(InvalidDataException e)
                {
                    return new BadRequestObjectResult(e.Message);
                }

                _watchService.DeleteAlarmSubscriptionFromWatch(aw);

                return new OkResult();
            }

            private AlarmWatch ParseFunctionModelToDtoModel(DeleteAlarmSubscriptionModel dasm)
            {
                AlarmWatch aw = _watchService.GetSubscriptionOfAlarmFromWatch(dasm.AlarmId, dasm.WatchId);
                return aw;
            }
    }
}