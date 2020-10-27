using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AlarmSystem.Core.Application;

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
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "machines")] HttpRequest req,
            ILogger log)
        {
            _machineService.CreateMachine();
            return new OkResult();
        }
    }
}