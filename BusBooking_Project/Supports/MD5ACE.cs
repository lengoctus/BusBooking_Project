using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Supports
{
    public class MD5ACE
    {
        public static string Encrypt(string text)
        {
            return GetMd5Hash(text);
        }

        private static string GetMd5Hash(string text)
        {
            using (MD5 md5hash = MD5.Create())
            {
                byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder strbuilder = new StringBuilder();
                foreach (byte item in data)
                {
                    strbuilder.Append(item.ToString("x2"));
                }
                return strbuilder.ToString();
            }
        }

        public static bool VerifyMD5(string text, string hash)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(GetMd5Hash(text), hash) == 0 ? true : false;
        }
    }
}
