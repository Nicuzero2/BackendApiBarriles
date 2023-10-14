using BackendApiBarriles.DAL.Accounts;
using Microsoft.Extensions.Configuration; // Asegúrate de importar la referencia correcta

namespace BackendApiBarriles.LayerBLAppBarriles.Accounts
{
    public class AccountsBL
    {
        private IConfiguration _configuration;

        public AccountsBL(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string[] CreateAccount(string user, string password, int numberCellphone, string email)
        {
            AccountsDAL accDal = new AccountsDAL(_configuration);
            BL.Util _bl = new BL.Util();
            var keyEncript = _configuration["AppSettings:EncryptionKey"];

            var passwordEncryptedFinal = _bl.Encrypt(password, keyEncript);

            return accDal.insertNewUser(user, passwordEncryptedFinal, email, numberCellphone);
        }

        public bool ValidateAccount(string user, string password)
        {
            AccountsDAL accDal = new AccountsDAL(_configuration);
            BL.Util _bl = new BL.Util();
            var keyEncript = _configuration["AppSettings:EncryptionKey"];

            var passwordEncripted = _bl.Encrypt(password, keyEncript);
            return accDal.validateUserPassword(user, passwordEncripted); 
        }
    }
}
