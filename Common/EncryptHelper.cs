using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EncryptHelper
    {
        public static string MD5Encrypt(string password)
        {
            StringBuilder tmp = new StringBuilder();

            if (!string.IsNullOrEmpty(password))
            {
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                byte[] hashedDataBytes;
                hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(password));

                foreach (byte i in hashedDataBytes)
                {
                    tmp.Append(i.ToString("x2"));
                }
            }

            return tmp.ToString();
        }
    }
}
