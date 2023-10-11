using BackendApiBarriles.LayerBLAppBarriles.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiBarriles.Controllers 
{ 
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {                
        [HttpGet("ValidateAccess")]
        public IActionResult ValidaAccesoLogin(string usuario, string password)
        {
            string[] objetoResponse = null;

            if (usuario == "Nico")
            {
                objetoResponse =  new string[]{
                    "codigo: 200",
                    "mensaje: Logeado correctamente"
                };
            }
            return Ok(objetoResponse);
        }

        [HttpGet("CreateAccounts")]
        public IActionResult CreateAccounts(string user, string password, int numberCellphone, string email)
        {
            AccountsBL accBL = new AccountsBL();
            return Ok(accBL.CreateAccount(user, password, numberCellphone, email));
        }
    }
}