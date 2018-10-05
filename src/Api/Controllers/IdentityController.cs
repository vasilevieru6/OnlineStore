using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet]
        public async Task<IActionResult> GetRequest()
        {
            var clients = await DiscoveryClient.GetAsync("http://localhost:5000");
            var tokenClient = new TokenClient(clients.TokenEndpoint, "angular", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("resourceApi");

            if (tokenResponse.IsError)
            {
                return new JsonResult(tokenResponse.Error);
            }

            return new JsonResult(tokenResponse.Json);
        }
    }
}