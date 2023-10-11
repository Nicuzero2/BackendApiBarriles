using BackendApiBarriles.DAL.Accounts;

namespace BackendApiBarriles.LayerBLAppBarriles.Accounts
{
    public class AccountsBL
    {
        public string[] CreateAccount(string user, string password, int numberCellphone, string email)
        {
            AccountsDAL accDal = new AccountsDAL();
            BL.Util _bl = new BL.Util();
            var valorConfigEcryptarPass = "0q347boasie";

            var passwordMezclated = _bl.MezclarClaveConPalabra(valorConfigEcryptarPass, password);
            var passwordEncryptedFinal = _bl.EncriptarClave(passwordMezclated);                       

            return accDal.insertNewUser(user, passwordEncryptedFinal, email, numberCellphone);
        }
    }
}
