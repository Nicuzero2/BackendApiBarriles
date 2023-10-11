using System.Security.Cryptography;
using System.Text;

namespace BackendApiBarriles.BL
{
    public class Util
    {
        public string EncriptarClave(string clave)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(clave);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public string MezclarClaveConPalabra(string claveEncriptada, string palabra)
        {
            return claveEncriptada + palabra;
        }
    }
}
