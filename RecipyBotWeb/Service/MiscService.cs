using System;
using System.Collections.Generic;
using System.Linq;
using RecipyBotWeb.Constants;

namespace RecipyBotWeb.Service
{
    public static class MiscService
    {
        public static IEnumerable<int> GiveXFromYNumbers(int x, int y)
        {
            List<int> rangeSet = new List<int>();
            for (int i=0; i<y; i++)
            {
                rangeSet.Add(i);
            }
            Random r = new Random();
            return rangeSet.Shuffle().Take((x >= y ? BotConstants.OtherConstants.DefaultTopN : x)); //myValues.OrderBy(x => r.Next()).Take(3);            
        }

        public static bool IsUserNameDefaultOrBlank(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            if (username.Equals("user", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }

        public static bool CompareTwoStrings(string a, string b)
        {
            if (a.Trim().Equals(b.Trim(), StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public static string GetRandomTagline()
        {
            string[] taglines = new string[] {
                "Delicious and tempting",
                "Perfect for guests"
            };
            foreach (var tagline in taglines.Shuffle().Take(1))
            {
                return tagline.ToString();
            }
            return "";
        }

        public static string GetRandomIngredientsPrefix()
        {
            string[] prefixes = new string[] {
                "Made with",
                "Ingredients:",
                "Ingredients are",
                "You will need"
            };
            foreach (var prefix in prefixes.Shuffle().Take(1))
            {
                return prefix.ToString() + " ";
            }
            return "";
        }

    }

    #region EXTENSIONS
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (rng == null) throw new ArgumentNullException("rng");

            return source.ShuffleIterator(rng);
        }

        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng)
        {
            List<T> buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
    #endregion
}