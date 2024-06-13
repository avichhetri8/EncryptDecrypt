namespace Helper
{
    public class Base64Helper
    {
        /// <summary>
        /// perform the task of decoding a Base64-encoded string back into its original byte array
        /// </summary>
        /// <param name="data"></param>
        /// <returns>array of bytes</returns>
        public static byte[] Base64Decode(string data)
        {
            return Convert.FromBase64String(data);

        }
        /// <summary>
        /// converts a byte array into a Base64-encoded string
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string</returns>
        public static string Base64Encode(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
    }
}
