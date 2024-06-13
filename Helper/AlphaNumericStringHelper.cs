using System.Text;

namespace Helper
{
    public class AlphaNumericStringHelper
    {
        /// <summary>
        /// provide the combination of Alphabate number
        /// </summary>
        /// <param name="size"></param>
        /// <returns>string</returns>
        public static string GetAlphaNumericString(int size)
        {
            string AlphaNumericString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "0123456789" + "abcdefghijklmnopqrstuvxyz";
            StringBuilder sb = new(size);
            Random rnd = new();
            for (int i = 0; i < size; i++)
            {
                var random = Math.Round(new decimal(rnd.NextDouble()), 5);
                int index = (int)(AlphaNumericString.Length * random);
                sb.Append(AlphaNumericString[index]);
            }
            return sb.ToString();
        }
    }
}
