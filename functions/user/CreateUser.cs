using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Core.Entity.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlarmSystem.Functions.User{
    public class CreateUser
    {
        private IUserService _userService;
        public CreateUser(IUserService userService){
            _userService = userService;
        }

        
        
        [FunctionName("CreateUser")]
       public async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")] HttpRequest req,
            ILogger log)
            {
                string reqBody = await new StreamReader(req.Body).ReadToEndAsync();
                var user = JsonConvert.DeserializeObject<UserToCreate>(reqBody);
                _userService.CreateUser(user);
                return new OkResult();
            }
    }
}