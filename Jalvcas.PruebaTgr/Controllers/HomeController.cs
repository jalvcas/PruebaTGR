using Jalvcas.PruebaTgr.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Jalvcas.PruebaTgr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> LoginAsync()
        {
            AuthenticateResult auth = await HttpContext.AuthenticateAsync();
            string idToken = auth.Properties.GetTokenValue(OpenIdConnectParameterNames.IdToken);

            return RedirectToAction("Landing", new { idToken });
        }

        public IActionResult Landing(string idToken)
        {
            if (idToken == null) return Error();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(idToken);
            var decodedToken = jsonToken as JwtSecurityToken;


            ViewData["IdToken"] = idToken;
            ViewData["Name"] = decodedToken.Claims.First(claim => claim.Type == "name").Value;
            ViewData["Email"] = decodedToken.Claims.First(claim => claim.Type == "email").Value;

            return View();
        }

        [HttpPost]
        public IActionResult Ticket()
        {
            return new JsonResult(Guid.NewGuid());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
