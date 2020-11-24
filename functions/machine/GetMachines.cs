using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AlarmSystem.Functions.Machine
{
    public class GetMachines
    {
        private IMachineService _machineService;
        
        public GetMachines(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [FunctionName("GetMachines")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "machines")] HttpRequest req,
            ILogger log)
        {
            List<AlarmSystem.Core.Entity.DB.Machine> machines = _machineService.GetMachines();

            if (machines.Count == 0) {
                return new NoContentResult();
            }

            return new OkObjectResult(machines);
        }
    }
}