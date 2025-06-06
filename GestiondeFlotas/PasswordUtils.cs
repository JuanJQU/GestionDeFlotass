using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeFlotas
{
    class PasswordUtils
    {
        public static byte[] GetSHA1Hash(string input)
        {
            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                return sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
        }
    }
}
