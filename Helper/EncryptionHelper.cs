using System.Security.Cryptography;

namespace Helper
{
    public class EncryptionHelper
    {
        public static string Encrypt<T>(string strToEncrypt, byte[] secretKey, byte[] iv, CipherMode mode, PaddingMode padding) where T : SymmetricAlgorithm, new()
        {
            byte[] array;

            using (T algorithm = new T())
            {
                algorithm.Key = secretKey;
                algorithm.IV = iv;
                algorithm.Mode = mode;
                algorithm.Padding = padding;

                ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new((Stream)memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new((Stream)cryptoStream))
                {
                    streamWriter.Write(strToEncrypt);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

    }
}
