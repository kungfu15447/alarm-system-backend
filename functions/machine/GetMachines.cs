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

        [FunctionName("CreateMachine")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "machines")] HttpRequest req,
            ILogger log)
        {
            _machineService.CreateMachine();
            return new OkResult();
        }
    }
}