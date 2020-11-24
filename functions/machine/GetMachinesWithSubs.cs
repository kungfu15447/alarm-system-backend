using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace functions.machine
{
    public class GetMachinesWithSubs
    {
        private IMachineService _machineService;
        public GetMachinesWithSubs(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [FunctionName("GetMachinesWithSubs")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "machines/{watchId}")] HttpRequest req,
            ILogger log, string watchId)
        {
            List<MachineWithSubscription> machinesWithSubs = _machineService.GetAllMachinesWithSubs(watchId);

            if (machinesWithSubs.Count == 0) {
                return new NoContentResult();
            }

            return new OkObjectResult(machinesWithSubs);
        }
    }
}