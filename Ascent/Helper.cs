using System;
using System.Collections.Generic;
using System.Text;

namespace Ascent
{
    public static class Helper
    {
        public static string GetMd5Hash(string input)
        {
            using (System.Security.Cryptography.MD5 md5hash = System.Security.Cryptography.MD5.Create())
            {
                byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}
