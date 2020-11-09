using System.Threading.Tasks;
using core.application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlarmSystem.Core.Entity.Dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
                string requestString = await req.ReadAsStringAsync();
                string requestJsonString = JsonConvert.SerializeObject(requestString);
                AlarmWatch aw = JsonConvert.DeserializeObject<AlarmWatch>(requestJsonString);


                _watchservice.SubscribeToAlarm(aw);
                return new OkResult();
            }
    }
}