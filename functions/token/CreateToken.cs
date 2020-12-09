using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Domain;
using Core.Entity.Dto.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlarmSystem.Functions.Token
{
    public class CreateToken
    {
        private IAuthenticationService _authService;
        private IUserService _userService;

        public CreateToken(IAuthenticationService authenticationService, IUserService userService)
        {
            _authService = authenticationService;
            _userService = userService;
        }

        [FunctionName("Token")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "token")] HttpRequest req,
            ILogger log)
        {
            string reqBody = await new StreamReader(req.Body).ReadToEndAsync();
            var model = JsonConvert.DeserializeObject<LoginInputModel>(reqBody);
            var user = _userService.GetUserByName(model.Name);

            if(user == null)
            {
                return new UnauthorizedResult();
            }
            if (!_authService.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new UnauthorizedResult();
            }
            user.PasswordSalt = user.PasswordHash = null;
            var token = _authService.GenerateToken(user);
            return new OkObjectResult(new 
            {
                name = user.Name,
                email = user.Email,
                token = token
            });
        }
    }
}