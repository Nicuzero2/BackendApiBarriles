using BackendApiBarriles.LayerBLAppBarriles.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BackendApiBarriles.Controllers 
{ 

    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("ValidateAccess")]
        public IActionResult ValidaAccesoLogin(string usuario, string password)
        {            
            AccountsBL accBL = new AccountsBL(_configuration);            

            return Ok(accBL.ValidateAccount(usuario,password));
        }

        [HttpGet("CreateAccounts")]
        public IActionResult CreateAccounts(string user, string password, int numberCellphone, string email)
        {
            AccountsBL accBL = new AccountsBL(_configuration);
            return Ok(accBL.CreateAccount(user, password, numberCellphone, email));
        }

        [HttpGet("PRUEBADELXEO")]
        public IActionResult test(int x, int y, int z)
        {
            if (x % 2 != 0)
            {
                x = x + 1;
            }

            y = y - 1;

            if (y % 2 != 0)
            {
                y = y + 1;
            }
            z = x + y;

            var response = new
            {
                X = x,
                Y = y,
                Z = z
            };

            return Ok(response);
        }

    }
}