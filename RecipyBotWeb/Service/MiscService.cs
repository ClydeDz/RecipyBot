using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }

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

}