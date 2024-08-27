using Helper;
using Microsoft.Extensions.Configuration;
namespace EncryptDecrypt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var privateKey = builder["RSAKeys:PrivateKey"];
            var publicKey = builder["RSAKeys:PublicKey"];

            var data = "helloworld";

            var alphaNumeric = AlphaNumericStringHelper.GetAlphaNumericString(32);
            byte[] secrect = SecreteKeyHelper.GetSecretKey(alphaNumeric);

            string base64EncodedSecretKey = Base64Helper.Base64Encode(secrect);
            string encryptedData = EncryptionHelper.Encrypt(data, secrect);
            string signature = SignatureHelper.GenerateSignature(encryptedData, privateKey);
            var verify = SignatureHelper.VerifySignature(encryptedData, publicKey, Base64Helper.Base64Decode(signature));

            if (verify)
            {
                var actualData = DecryptionHelper.Decrypt(encryptedData, secrect);
                Console.Write("Encrypted  Data is: " + actualData);
                //var outputData = DecryptionHelper.Decrypt(actualData, privateKey);
            }
            else
            {
                Console.Write("Signature Verification Failed. Could not Decrypt !!!!");
            }
        }
    }
}
