using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhy.Components.Configuration.Test
{
    /// <summary>
    /// 加密器
    /// </summary>
    public class TestEncipher : IConfigurationEncipher
    {
        public string Decrypt(string ciphertext)
        {
            if (string.IsNullOrEmpty(ciphertext))
            {
                return string.Empty;
            }
            return ciphertext.Replace("加密了：", "");
        }

        public string Encrypt(string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext))
            {
                return string.Empty;
            }
            return "加密了：" + plaintext;
        }
    }
}
