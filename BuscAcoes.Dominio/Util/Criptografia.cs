using System.Security.Cryptography;
using System.Text;

namespace BuscAcoes.Dominio.Util
{
    public static class Criptografia
    {
        public static string getMdIHash(string Senha)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(Senha));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
