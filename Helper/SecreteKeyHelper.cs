namespace Helper
{
    public class SecreteKeyHelper
    {
        public static byte[] GetSecretKey(string secretKey)
        {
            byte[] decodeSecretKey = Base64Helper.Base64Decode(secretKey);
            return decodeSecretKey;
        }
    }
}
