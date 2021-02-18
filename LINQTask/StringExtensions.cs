using System.Linq;
using System.Text;

namespace System
{
    public static class StringExtensions
    {
        public static bool CheckOccurrence(this string str, int count)
        {
            if (String.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(nameof(str), "Data is null");

            str = str.ToLower();
            var sb = new StringBuilder(str);
            var uniqueSb = new StringBuilder();


            for (int i = 0; i < sb.Length; i++)
            {
                if (uniqueSb.ToString().Contains(sb[i]))
                    continue;
				
                uniqueSb.Append(sb[i]);
				
                if (str.Count(s => s == sb[i]) == count)
                    return true;
            }

            return false;
        }
    }
}