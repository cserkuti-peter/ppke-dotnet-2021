using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyFirstClassLibrary
{
    public class Helper
    {
        public static List<string> GetWords(string text1)
        {
            if (text1 == null)
                throw new ArgumentNullException(nameof(text1));

            var pattern = @"\w+";
            var matches = Regex.Matches(text1, pattern);
            var list = new List<string>();
            foreach (Match match in matches)
            {
                list.Add(match.Value);
            }
            return list;
        }
    }
}
