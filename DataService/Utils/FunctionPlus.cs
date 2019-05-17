using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace DataService.Utils
{
    public class FunctionPlus
    {
        public string GetMd5HashData(string password)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(password)).Select(p => p.ToString("x2")));
        }
        public string GetRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Range(1, 8).Select(s => chars[random.Next(chars.Length)]).ToArray());
        }
    }

}
