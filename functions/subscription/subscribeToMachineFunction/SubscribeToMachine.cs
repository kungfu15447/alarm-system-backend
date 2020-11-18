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
using System;
using AlarmSystem.Core.Application.Exception;

namespace AlarmSystem.Functions.Subscription.SubscribeToMachineFunction {

    public class SubscribeToMachine {
        private IWatchService _watchservice;
        private IMachineService _machineService;

        public SubscribeToMachine(IWatchService watchService, IMachineService machineService) {
            _watchservice = watchService;
            _machineService = machineService;
        }

        [FunctionName("SubscribeToMachine")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "subscribeToMachine")] HttpRequest req,
            ILogger log) 
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SubscribeToMachineModel stmm = JsonConvert.DeserializeObject<SubscribeToMachineModel>(requestBody);
            
            try
            {
                MachineWatch mw = ParseFunctionModelToDtoModel(stmm);
                _watchservice.SubscribeToMachine(mw);
                return new OkResult();
            }
            catch (EntityNotFoundException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        private MachineWatch ParseFunctionModelToDtoModel(SubscribeToMachineModel stmm)
        {
            AlarmSystem.Core.Entity.Dto.Machine machine = _machineService.GetMachineById(stmm.MachineId);

            MachineWatch mw = new MachineWatch()
            {
                Machine = machine,
                WatchId = stmm.WatchId
            };

            return mw;
        }
    }
}

