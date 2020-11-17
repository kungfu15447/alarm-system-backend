using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlarmSystem.Core.Entity.Dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using AlarmSystem.Core.Application;

namespace AlarmSystem.Functions.Watch {

    public class SubscribeToAlarm {
        private IWatchService _watchservice;

        public SubscribeToAlarm(IWatchService watchService) {
            _watchservice = watchService;
        }

        [FunctionName("SubscribeToAlarm")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "subscribeToAlarm")] HttpRequest req,
            ILogger log) 
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                AlarmWatch aw = JsonConvert.DeserializeObject<AlarmWatch>(requestBody);
                _watchservice.SubscribeToAlarm(aw);
                return new OkResult();
            }
    }
}