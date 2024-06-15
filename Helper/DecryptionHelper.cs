using System.Security.Cryptography;

namespace Helper
{
    public class DecryptionHelper
    {
        public static string Decrypt<T>(string strToDecrypt, byte[] secretKey, byte[] iv, CipherMode mode, PaddingMode padding) where T : SymmetricAlgorithm, new()
        {
            byte[] buffer = Convert.FromBase64String(strToDecrypt);

            using (T algorithm = new T())
            {
                algorithm.Key = secretKey;
                algorithm.IV = iv;
                algorithm.Mode = mode;
                algorithm.Padding = padding;

                ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

                using MemoryStream memoryStream = new(buffer);
                using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
                using StreamReader streamReader = new(cryptoStream);
                return streamReader.ReadToEnd();
            }
        }
    }
}
