using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;

namespace Helper
{
    public class EncryptionHelper
    {
        /*public static string Encrypt<T>(string strToEncrypt, byte[] secretKey, byte[] iv, CipherMode mode, PaddingMode padding) where T : SymmetricAlgorithm, new()
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
*/
        public static string Encrypt(string strToEncrypt, byte[] secretKey)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = secretKey;
                aes.IV = iv;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(strToEncrypt);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);

        }


        public static byte[] Encrypt(string data, string PubKey)
        {
            StringBuilder publicKey = new StringBuilder();
            publicKey.Append("-----BEGIN PUBLIC KEY-----\r\n");
            publicKey.Append(PubKey);
            publicKey.Append("\r\n-----END PUBLIC KEY-----");


            using TextReader publicKeyTextReader = new StringReader(publicKey.ToString());
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)new PemReader(publicKeyTextReader).ReadObject();

            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters(publicKeyParam);

            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters(rsaParams);

            var result1 = csp.Encrypt(Encoding.UTF8.GetBytes(data), false);

            var output2 = Base64Helper.Base64Encode(result1);

            var encryptedBytes = csp.Encrypt(Encoding.UTF8.GetBytes(data), false);
            var oooo = Base64Helper.Base64Encode(encryptedBytes);

            return result1;

        }

    }
}
