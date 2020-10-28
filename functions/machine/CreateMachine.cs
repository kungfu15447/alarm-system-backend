using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AlarmSystem.Core.Application;
using System.Collections.Generic;

namespace AlarmSystem.Functions.Machine
{
    public class CreateMachine
    {
        private IMachineService _machineService;
        public CreateMachine(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [FunctionName("CreateMachine")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "machines")] HttpRequest req,
            ILogger log)
        {
            List<AlarmSystem.Core.Entity.Dto.Machine> machines = _machineService.GetMachines();

            if (machines.Count == 0) {
                return new NoContentResult();
            }
            
            return new OkObjectResult(machines);
        }
    }
}