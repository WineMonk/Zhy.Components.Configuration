namespace Zhy.Components.Configuration
{
    /// <summary>
    /// 配置加密器
    /// </summary>
    public interface IConfigurationEncipher
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns>密文</returns>
        string Encrypt(string plaintext);
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <returns>明文</returns>
        string Decrypt(string ciphertext);
    }
}
