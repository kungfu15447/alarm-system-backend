using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AlarmSystem.Core.Application;
using System.IO;
using Newtonsoft.Json;

namespace AlarmSystem.Functions.Machine.CreateMachineFunction
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
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var cmm = JsonConvert.DeserializeObject<CreateMachineModel>(requestBody);
            
            AlarmSystem.Core.Entity.DB.Machine machine = new AlarmSystem.Core.Entity.DB.Machine
            {
                Name = cmm.Name,
                Type = cmm.Type
            };

            try 
            {
                _machineService.CreateMachine(machine);
                return new OkResult();
            } catch (InvalidDataException ex) {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}