using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class SignatureHelper
    {
        public static string generateSignature(string data, string privateKeyPath)
        {
            StringBuilder pri = new StringBuilder();
            pri.Append("-----BEGIN RSA PRIVATE KEY-----\r\n");
            pri.Append(privateKeyPath);
            pri.Append("\r\n-----END RSA PRIVATE KEY-----");


            string key = pri.ToString();
            using (TextReader strReader = new StringReader(key))
            {

                PemReader pemReader = new PemReader(strReader);
                AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
                RsaKeyParameters privateKey = (RsaKeyParameters)keyPair.Private;


                var outp = Encoding.ASCII.GetBytes(data);

                ISigner sig = SignerUtilities.GetSigner("SHA256withRSA");
                sig.Init(true, privateKey);
                sig.BlockUpdate(outp, 0, outp.Length);
                byte[] signedBytes = sig.GenerateSignature();

                var output = Base64Helper.Base64Encode(signedBytes);

                return output;
            }

        }

        public static bool verifySignature(string data, string publicKeyPath, byte[] signature)
        {
            StringBuilder publicKeyPemStr = new StringBuilder();
            publicKeyPemStr.Append("-----BEGIN PUBLIC KEY-----\r\n");
            publicKeyPemStr.Append(publicKeyPath);
            publicKeyPemStr.Append("\r\n----------END PUBLIC KEY----------");


            string key = publicKeyPemStr.ToString();

            using (TextReader publicKeyTextReader = new StringReader(key))
            {
                var re = Encoding.ASCII.GetBytes(data);

                PemReader pemReader = new PemReader(publicKeyTextReader);
                RsaKeyParameters publicKey = (RsaKeyParameters)((AsymmetricKeyParameter)pemReader.ReadObject());

                ISigner sig = SignerUtilities.GetSigner("SHA256withRSA");
                sig.Init(false, publicKey);
                sig.BlockUpdate(re, 0, re.Length);

                if (sig.VerifySignature(signature))
                {
                    Console.WriteLine("Ok");
                    return true;
                }
                else
                {
                    Console.WriteLine("NOK");
                    return false;
                }

            }
        }

    }
}
