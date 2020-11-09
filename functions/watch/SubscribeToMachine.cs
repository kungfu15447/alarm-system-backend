using System.Threading.Tasks;
using core.application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlarmSystem.Core.Entity.Dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;

namespace AlarmSystem.Functions.Watch {

    public class SubscribeToMachine {
        private IWatchService _watchservice;

        public SubscribeToMachine(IWatchService watchService) {
            _watchservice = watchService;
        }

        [FunctionName("SubscribeToMachine")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "subscribeToMachine")] HttpRequest req,
            ILogger log) 
        {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                MachineWatch mw = JsonConvert.DeserializeObject<MachineWatch>(requestBody);
                _watchservice.SubscribeToMachine(mw);
                
                return new OkResult();
        }
    }
}

