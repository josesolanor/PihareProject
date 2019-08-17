using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPihareii.Core
{
    public class Hash
    {
        public string EncryptString(string value)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }
    }
}
