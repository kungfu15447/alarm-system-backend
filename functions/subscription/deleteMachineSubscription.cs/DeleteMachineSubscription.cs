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

namespace AlarmSystem.Functions.Subscription.DeleteMachineSubscription
{
    public class DeleteMachineSubscription
    {
        private IWatchService _watchService;
        public DeleteMachineSubscription(IWatchService watchService)
        {
            _watchService = watchService;
        }

        //TODO Validate that machinewatch exists
        [FunctionName("DeleteMachineSubscription")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "machinesubs")] HttpRequest req,
            ILogger log)
        {
            string reqBody = await new StreamReader(req.Body).ReadToEndAsync();
            var model = JsonConvert.DeserializeObject<DeleteMachineSubscriptionModel>(reqBody);

            MachineWatch mw;
            
            try 
            {
                mw = GetSpecificSubscription(model);
            } catch(InvalidDataException e) 
            {
                return new BadRequestObjectResult(e);
            }

            _watchService.DeleteMachineSubscriptionFromWatch(mw);

            return new OkResult();
        }

        private MachineWatch GetSpecificSubscription(DeleteMachineSubscriptionModel model)
        {
            string machineId = model.MachineId;
            string watchId = model.WatchId;

            return _watchService.GetMachineSubcriptionOfMachineFromWatch(machineId, watchId);
        }
    }
}