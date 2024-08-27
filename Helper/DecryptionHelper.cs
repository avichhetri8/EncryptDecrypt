using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.OpenSsl;

namespace Helper
{
    public class DecryptionHelper
    {
        //public static string Decrypt<T>(string strToDecrypt, byte[] secretKey, byte[] iv, CipherMode mode, PaddingMode padding) where T : SymmetricAlgorithm, new()
        //{
        //    byte[] buffer = Convert.FromBase64String(strToDecrypt);

        //    using (T algorithm = new T())
        //    {
        //        algorithm.Key = secretKey;
        //        algorithm.IV = iv;
        //        algorithm.Mode = mode;
        //        algorithm.Padding = padding;

        //        ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

        //        using MemoryStream memoryStream = new(buffer);
        //        using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
        //        using StreamReader streamReader = new(cryptoStream);
        //        return streamReader.ReadToEnd();
        //    }
        //}

        public static string Decrypt(string strToDecrypt, byte[] secretKey)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(strToDecrypt);

            using Aes aes = Aes.Create();
            aes.Key = secretKey;
            aes.IV = iv;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;


            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new(buffer);
            using CryptoStream cryptoStream = new((Stream)memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new((Stream)cryptoStream);
            return streamReader.ReadToEnd();


        }


        public static string Decrypt(byte[] secretKey, string clientPrivateKey)
        {
            StringBuilder privateStr = new StringBuilder();
            privateStr.Append("-----BEGIN RSA PRIVATE KEY-----\r\n");
            privateStr.Append(clientPrivateKey);
            privateStr.Append("\r\n-----END RSA PRIVATE KEY-----");

            using TextReader strReader = new StringReader(privateStr.ToString());

            PemReader pemReader = new PemReader(strReader);
            AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();

            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)keyPair.Private);
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters(rsaParams);

            var decryptedBytes = csp.Decrypt(secretKey, false);
            return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);

        }

    }
}
